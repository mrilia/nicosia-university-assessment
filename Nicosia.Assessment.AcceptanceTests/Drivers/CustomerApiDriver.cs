using FluentAssertions;
using Nicosia.Assessment.AcceptanceTests.Models;
using System.Net.Http.Json;
using TechTalk.SpecFlow.Assist;

namespace Nicosia.Assessment.AcceptanceTests.Drivers
{
    public class CustomerApiDriver
    {
        private readonly HttpClient _httpClient;
        private readonly ScenarioContext _scenarioContext;

        public CustomerApiDriver(HttpClient httpClient, ScenarioContext scenarioContext)
        {
            _httpClient = httpClient;
            _scenarioContext = scenarioContext;
        }

        public async Task CreateCustomers(Table table)
        {
            var creatCustomerRequests = table.CreateSet<CreateCustomerRequest>();
            var createdCustomers = new List<Customer>();
            var responses = new List<HttpResponseMessage>();

            foreach (var createCustomerRequest in creatCustomerRequests)
            {
                var response = await _httpClient.PostAsJsonAsync("customer", createCustomerRequest);
                var responseCustomer = await response.Content.ReadFromJsonAsync<Customer>();

                responses.Add(response);

                if (response.IsSuccessStatusCode)
                {
                    createdCustomers.Add(responseCustomer!);
                }
            }

            if (createdCustomers.Any())
            {
                _scenarioContext["CreatedCustomers"] = createdCustomers;
            }

            _scenarioContext["Responses"] = responses;
        }

        public async Task<List<Customer>> GetCustomerByEmail(string email)
        {
            var query = new GetCustomerListRequest()
            {
                Email = email
            };

            var response = await _httpClient.GetAsync($"customer/list?email={query.Email}");

            List<Customer>? fetchedCustomers = await response.Content.ReadFromJsonAsync<List<Customer>>();

            return fetchedCustomers!;
        }


        public void CheckTheErrorCode(string errorCode)
        {
            var responses = _scenarioContext.Get<List<HttpResponseMessage>>("Responses");

            foreach (var response in responses)
            {
                ((int)(response.StatusCode)).Should().Be(int.Parse(errorCode));
            }
        }

        public void UpdateCustomerEmail(string currentEmail, string newEmail)
        {
            var createdCustomers = _scenarioContext.Get<List<Customer>>("CreatedCustomers");
            var customerBeforUpdate = createdCustomers.First(f => f.Email == currentEmail);

            var customerToUpdate = new UpdateCustomerRequest()
            {
                Id = customerBeforUpdate.Id,
                Firstname = $"{customerBeforUpdate.Firstname}",
                Lastname = $"{customerBeforUpdate.Lastname}",
                Email = newEmail,
                DateOfBirth = customerBeforUpdate.DateOfBirth,
                PhoneNumber = $"+{customerBeforUpdate.PhoneNumber}",
                BankAccountNumber = customerBeforUpdate.BankAccountNumber
            };

            var response = _httpClient.PutAsJsonAsync("customer", customerToUpdate).Result;

            _scenarioContext["Responses"] = new List<HttpResponseMessage>() { response };
        }

        public async Task DeleteCustomer(string email)
        {
            var customerToDelete = await GetCustomerByEmail(email);

            var response = _httpClient.DeleteAsync($"customer/{customerToDelete?.First().Id}").Result;

            _scenarioContext["Responses"] = new List<HttpResponseMessage>() { response };
        }

        public void CheckTheResponse(string message)
        {
            var responses = _scenarioContext.Get<List<HttpResponseMessage>>("Responses");

            foreach (var response in responses)
            {
                response.Content.ReadAsStringAsync().Result.ToLower().Should().Contain(message.ToLower());
            }
        }
    }
}