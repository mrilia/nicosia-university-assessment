using Nicosia.Assessment.Shared.Validators;
using Xunit;

namespace Nicosia.Assessment.AcceptanceTests
{
    public class MobileValidatorTest
    {
        [Theory]
        [InlineData("+989121234567", true)]
        [InlineData("+982188228866", false)]
        [InlineData("+31612345678", true)]
        public void MobileValidatorTest_WithExpectedResult(string phoneNumber, bool expectedResult)
        {
            bool testResult = PhoneNumberValidator.IsValid(phoneNumber);
            
            Assert.Equal(expectedResult, testResult);
        }
    }
}