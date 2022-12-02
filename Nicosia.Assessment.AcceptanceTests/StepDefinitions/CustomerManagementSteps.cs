using FluentAssertions;
using Nicosia.Assessment.AcceptanceTests.Drivers;

namespace Nicosia.Assessment.AcceptanceTests.StepDefinitions
{
    [Binding]
    public class CustomerManagementSteps
    {
        
        private readonly CustomerApiDriver _customerApiDriver;

        public CustomerManagementSteps(CustomerApiDriver customerApiDriver)
        {
            _customerApiDriver = customerApiDriver;
        }

        [When(@"user creates a customer with the following details")]
        public async Task WhenUserCreatesACustomerWithTheFollowingDetails(Table table)
        {
            await _customerApiDriver.CreateCustomers(table);
        }


        [Then(@"user can lookup all customers and filter by Email of ""([^""]*)"" and get ""([^""]*)"" records")]
        public async Task ThenUserCanLookupAllCustomersAndFilterByEmailOfAndGetRecords(string email, string rowCount)
        {
            var fetchedCustomers = await _customerApiDriver.GetCustomerByEmail(email);

            fetchedCustomers?.Count.ToString().Should().Be(rowCount);
        }

       

        [Then(@"system must respond with error code of ""([^""]*)""")]
        public void ThenSystemMustRespondWithErrorCodeOf(string errorCode)
        {
            _customerApiDriver.CheckTheErrorCode(errorCode);
        }


        [When(@"user edit customer who has email of ""([^""]*)"" with new email of ""([^""]*)""")]
        public void WhenUserEditCustomerWhoHasEmailOfWithNewEmailOf(string currentEmail, string newEmail)
        {
            _customerApiDriver.UpdateCustomerEmail(currentEmail, newEmail);
        }


        [When(@"user delete customer by Email of ""([^""]*)""")]
        public async Task WhenUserDeleteCustomerByEmailOf(string email)
        {
            await _customerApiDriver.DeleteCustomer(email);
        }
        
        [Given(@"system has existing customer")]
        public async Task GivenSystemHasExistingCustomer(Table table)
        {
            await _customerApiDriver.CreateCustomers(table);
        }

        [Then(@"system response contains ""([^""]*)""")]
        public void ThenSystemResponseContains(string message)
        {
            _customerApiDriver.CheckTheResponse(message);
        }
    }
}
