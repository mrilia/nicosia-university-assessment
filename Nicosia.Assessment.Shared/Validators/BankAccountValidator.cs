
using IbanNet;
using System;

namespace Nicosia.Assessment.Shared.Validators
{
    public static class BankAccountValidator
    {
        public static bool IsValid(string bankAccountToCheck)
        {
            bankAccountToCheck = bankAccountToCheck.ToUpper();
            if (String.IsNullOrEmpty(bankAccountToCheck))
                return false;

            var validator = new IbanValidator();
            ValidationResult validationResult = validator.Validate(bankAccountToCheck);

            return validationResult.IsValid;
        }
    }
}
