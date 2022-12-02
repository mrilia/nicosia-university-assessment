

namespace Nicosia.Assessment.Shared.Validators
{
    public static class EmailValidator
    {
        public static bool IsValid(string emailToCheck)
        {
            var trimmedEmail = emailToCheck.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                return false;
            }

            try
            {
                var addr = new System.Net.Mail.MailAddress(emailToCheck);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }
    }
}
