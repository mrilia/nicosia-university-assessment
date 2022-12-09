using System;
using Castle.Core.Configuration;
using FluentValidation.TestHelper;
using Nicosia.Assessment.Application.Handlers.Student.Commands.AddNewStudent;
using Nicosia.Assessment.Application.Validators.Student;
using Nicosia.Assessment.Persistence.Context;
using Xunit;

namespace Nicosia.Assessment.AcceptanceTests.Student
{
    public class StudentValidatorTests
    {
        private readonly AddNewStudentCommandValidator _validator;

        public StudentValidatorTests()
        {
            var context = new SqliteDbContext();
            _validator = new AddNewStudentCommandValidator(context);
        }

        [Fact]
        public void When_Name_Is_Empty_ValidationFailed()
        {
            var model = new AddNewStudentCommand()
            {
                Firstname = "",
                Lastname = "",
                DateOfBirth = new DateOnly(2001,11,11),
                Email = "email@email.com",
                PhoneNumber = "+989121231234",
                Password = "password"
            };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.Firstname);
        }
    }
}
