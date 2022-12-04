using FluentValidation.TestHelper;
using Nicosia.Assessment.Application.Handlers.Student.Commands.AddNewStudent;
using Nicosia.Assessment.Application.Validators.Customer;
using Nicosia.Assessment.Persistence.Context;
using Xunit;

namespace Nicosia.Assessment.AcceptanceTests.Customer
{
    public class CustomerValidatorTests
    {
        private readonly AddNewStudentCommandValidator _validator;

        public CustomerValidatorTests()
        {
            var context = new SqliteDbContext();
            _validator = new AddNewStudentCommandValidator(context);
        }

        [Fact]
        public void When_Name_Is_Empty_ValidationFailed()
        {
            var model = new AddNewStudentCommand();

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.Firstname);
        }
    }
}
