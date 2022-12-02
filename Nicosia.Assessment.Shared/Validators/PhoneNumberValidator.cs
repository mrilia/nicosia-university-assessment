
using PhoneNumbers;

namespace Nicosia.Assessment.Shared.Validators
{
    public static class PhoneNumberValidator
    {
        private static readonly PhoneNumberUtil PhoneNumberUtil = PhoneNumberUtil.GetInstance();
        public static bool IsValid(string phoneNumberToCheck)
        {
            try
            {
                var phone = PhoneNumberUtil.Parse(phoneNumberToCheck, null);
                return PhoneNumberUtil.GetNumberType(phone) == PhoneNumberType.MOBILE;
            }
            catch
            {
                return false;
            }
        }
    }
}
