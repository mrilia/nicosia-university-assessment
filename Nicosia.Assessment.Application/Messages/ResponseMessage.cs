
namespace Nicosia.Assessment.Application.Messages
{
    public static class ResponseMessage
    {
        public static string CustomerNotFound => "Customer not found!";
        public static string EmailIsRequired => "Email is required";
        public static string PhoneNumberIsRequired => "Phone number is required";
        public static string BankAccountNumberIsRequired => "Bank account number is required";
        public static string FirstnameIsRequired => "Firstname is required";
        public static string LastnameIsRequired => "Lastname is required";
        public static string DateOfBirthIsRequired => "Date of birth is required";
        public static string CustomerIdNotExists => "Customer id not exists";
        public static string CustomerExists => "Duplicate customer by First-name, Last-name, Date-of-Birth";
        public static string EmailExists => "Duplicate customer by Email address";
        public static string EmailNotValid => "Invalid Email address";
        public static string BankAccountNumberNotValid => "Invalid Bank Account Number";
        public static string PhoneNumberNotValid => "Invalid Mobile Number";
    }
}
