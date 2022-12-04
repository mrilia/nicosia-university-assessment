using FluentAssertions;
using Nicosia.Assessment.AcceptanceTests.Models;
using System.Net.Http.Json;
using Nicosia.Assessment.Domain.Models.User;
using TechTalk.SpecFlow.Assist;
using Student = Nicosia.Assessment.AcceptanceTests.Models.Student;

namespace Nicosia.Assessment.AcceptanceTests.Drivers
{
    public class NicosiaAssessmentApiDriver
    {
        private readonly HttpClient _httpClient;
        private readonly ScenarioContext _scenarioContext;

        public NicosiaAssessmentApiDriver(HttpClient httpClient, ScenarioContext scenarioContext)
        {
            _httpClient = httpClient;
            _scenarioContext = scenarioContext;
        }

        public async Task CreateStudents(Table table)
        {
            var creatStudentRequests = table.CreateSet<CreateStudentRequest>();
            var createdStudents = new List<Models.Student>();
            var responses = new List<HttpResponseMessage>();

            foreach (var createStudentRequest in creatStudentRequests)
            {
                var response = await _httpClient.PostAsJsonAsync("student", createStudentRequest);
                var responseStudent = await response.Content.ReadFromJsonAsync<Student>();

                responses.Add(response);

                if (response.IsSuccessStatusCode)
                {
                    createdStudents.Add(responseStudent!);
                }
            }

            if (createdStudents.Any())
            {
                _scenarioContext["CreatedStudents"] = createdStudents;
            }

            _scenarioContext["Responses"] = responses;
        }

        public async Task<List<Student>> GetStudentByEmail(string email)
        {
            var query = new GetStudentListRequest()
            {
                Email = email
            };

            var response = await _httpClient.GetAsync($"student/list?email={query.Email}");

            List<Student>? fetchedStudents = await response.Content.ReadFromJsonAsync<List<Student>>();

            return fetchedStudents!;
        }


        public void CheckTheErrorCode(string errorCode)
        {
            var responses = _scenarioContext.Get<List<HttpResponseMessage>>("Responses");

            foreach (var response in responses)
            {
                ((int)(response.StatusCode)).Should().Be(int.Parse(errorCode));
            }
        }

        public void UpdateStudentEmail(string currentEmail, string newEmail)
        {
            var createdStudents = _scenarioContext.Get<List<Student>>("CreatedStudents");
            var studentBeforUpdate = createdStudents.First(f => f.Email == currentEmail);

            var studentToUpdate = new UpdateStudentRequest()
            {
                StudentId = studentBeforUpdate.StudentId,
                Firstname = $"{studentBeforUpdate.Firstname}",
                Lastname = $"{studentBeforUpdate.Lastname}",
                Email = newEmail,
                DateOfBirth = studentBeforUpdate.DateOfBirth,
                PhoneNumber = $"+{studentBeforUpdate.PhoneNumber}"
            };

            var response = _httpClient.PutAsJsonAsync("student", studentToUpdate).Result;

            _scenarioContext["Responses"] = new List<HttpResponseMessage>() { response };
        }

        public async Task DeleteStudent(string email)
        {
            var studentToDelete = await GetStudentByEmail(email);

            var response = _httpClient.DeleteAsync($"student/{studentToDelete?.First().StudentId}").Result;

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