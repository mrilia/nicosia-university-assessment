using FluentAssertions;
using Nicosia.Assessment.AcceptanceTests.Drivers;

namespace Nicosia.Assessment.AcceptanceTests.StepDefinitions
{
    [Binding]
    public class StudentManagementSteps
    {
        
        private readonly NicosiaAssessmentApiDriver _nicosiaAssessmentApiDriver;

        public StudentManagementSteps(NicosiaAssessmentApiDriver nicosiaAssessmentApiDriver)
        {
            _nicosiaAssessmentApiDriver = nicosiaAssessmentApiDriver;
        }

        [When(@"user login with following details")]
        public void WhenUserLoginWithFollowingDetails(Table table)
        {
            _nicosiaAssessmentApiDriver.Login(table);
        }

        [When(@"user creates a student with the following details")]
        public async Task WhenUserCreatesAStudentWithTheFollowingDetails(Table table)
        {
            await _nicosiaAssessmentApiDriver.CreateStudents(table);
        }


        [Then(@"user can lookup all students and filter by Email of ""([^""]*)"" and get ""([^""]*)"" records")]
        public async Task ThenUserCanLookupAllStudentsAndFilterByEmailOfAndGetRecords(string email, string rowCount)
        {
            var fetchedStudents = await _nicosiaAssessmentApiDriver.GetStudentByEmail(email);

            fetchedStudents?.Count.ToString().Should().Be(rowCount);
        }

       

        [Then(@"system must respond with code of ""([^""]*)""")]
        public void ThenSystemMustRespondWithCodeOf(string errorCode)
        {
            _nicosiaAssessmentApiDriver.CheckTheErrorCode(errorCode);
        }


        [When(@"user edit student who has email of ""([^""]*)"" with new email of ""([^""]*)""")]
        public void WhenUserEditStudentWhoHasEmailOfWithNewEmailOf(string currentEmail, string newEmail)
        {
            _nicosiaAssessmentApiDriver.UpdateStudentEmail(currentEmail, newEmail);
        }


        [When(@"user delete student by Email of ""([^""]*)""")]
        public async Task WhenUserDeleteStudentByEmailOf(string email)
        {
            await _nicosiaAssessmentApiDriver.DeleteStudent(email);
        }
        
        [Given(@"system has existing student")]
        public async Task GivenSystemHasExistingStudent(Table table)
        {
            await _nicosiaAssessmentApiDriver.CreateStudents(table);
        }

        [Then(@"system response contains ""([^""]*)""")]
        public void ThenSystemResponseContains(string message)
        {
            _nicosiaAssessmentApiDriver.CheckTheResponse(message);
        }
    }
}
