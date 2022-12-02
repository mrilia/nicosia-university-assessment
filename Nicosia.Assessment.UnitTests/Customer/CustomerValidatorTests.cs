using FluentValidation.TestHelper;
using Nicosia.Assessment.Application.Handlers.Customer.Commands.AddNewCustomer;
using Nicosia.Assessment.Application.Interfaces;
using Nicosia.Assessment.Application.Validators.Customer;
using Nicosia.Assessment.Persistence.Context;
using Xunit;

namespace Nicosia.Assessment.AcceptanceTests.Customer
{
    public class CustomerValidatorTests
    {
        private readonly AddNewCustomerCommandValidator _validator;

        public CustomerValidatorTests()
        {
            ICustomerContext context = new CustomerContext();
            _validator = new AddNewCustomerCommandValidator(context);
        }

        [Fact]
        public void When_Name_Is_Empty_ValidationFailed()
        {
            var model = new AddNewCustomerCommand();

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.Firstname);
        }
    }
}
