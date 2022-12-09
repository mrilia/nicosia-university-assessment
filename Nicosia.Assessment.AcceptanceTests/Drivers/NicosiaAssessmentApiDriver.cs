using System.Net.Http.Headers;
using FluentAssertions;
using Nicosia.Assessment.AcceptanceTests.Models;
using System.Net.Http.Json;
using Nicosia.Assessment.AcceptanceTests.Models.PaginationResponse;
using TechTalk.SpecFlow.Assist;
using System.Net.Http.Formatting;

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

            _scenarioContext["Responses"] = new List<HttpResponseMessage>();

            _httpClient.DefaultRequestHeaders
                .Accept
                .Add(new MediaTypeWithQualityHeaderValue("*/*"));

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json;");

            _scenarioContext.TryGetValue("authenticationInfo", out AuthenticateStudentResponse authenticationResponse);
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Authorization", authenticationResponse?.JwtToken);

        }

        public void Login(Table table)
        {
                var authenticateStudentRequests = table.CreateSet<AuthenticateStudentRequest>();
                var responses = new List<HttpResponseMessage>();

                var authenticateStudentRequest = authenticateStudentRequests.FirstOrDefault();
                
                var response = _httpClient.PostAsJsonAsync("api/user/v1/Authenticate/Admin/authenticate", authenticateStudentRequest).Result;
                var authenticationResponse = response.Content.ReadFromJsonAsync<AuthenticateStudentResponse>().Result;

                responses.Add(response);

                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Authorization", authenticationResponse?.JwtToken);

                _scenarioContext["authenticationInfo"] = authenticationResponse;
                _scenarioContext["Responses"] = responses;
            
        }
        public async Task CreateStudents(Table table)
        {
            var createStudentRequests = table.CreateSet<CreateStudentRequest>();
            var createdStudents = new List<Models.Student>();
            var responses = new List<HttpResponseMessage>();

            foreach (var createStudentRequest in createStudentRequests)
            {
                var response = await _httpClient.PostAsJsonAsync("api/deputy/v1/Student", createStudentRequest);
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

            var response = await _httpClient.GetAsync($"api/deputy/v1/Student/list?email={query.Email}");

            PaginationResponse<Student>? fetchedStudents = await response.Content.ReadFromJsonAsync<PaginationResponse<Student>>();

            return fetchedStudents!.Data.Items.ToList();
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
                PhoneNumber = $"{studentBeforUpdate.PhoneNumber}",
                Password = "P@s$w0rD",
            };

            var response = _httpClient.PutAsJsonAsync("api/deputy/v1/Student",studentToUpdate).Result;

            _scenarioContext["Responses"] = new List<HttpResponseMessage>() { response };
        }

        public async Task DeleteStudent(string email)
        {
            var studentToDelete = await GetStudentByEmail(email);

            var response = _httpClient.DeleteAsync($"api/deputy/v1/Student/{studentToDelete?.First().StudentId}").Result;

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


        public class NoCharSetJsonMediaTypeFormatter : JsonMediaTypeFormatter
        {
            public override void SetDefaultContentHeaders(Type type, HttpContentHeaders headers, MediaTypeHeaderValue mediaType)
            {
                base.SetDefaultContentHeaders(type, headers, mediaType);
                headers.ContentType.CharSet = "";
            }
        }
    }
}