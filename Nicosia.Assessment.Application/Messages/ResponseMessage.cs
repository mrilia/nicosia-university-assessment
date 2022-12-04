
namespace Nicosia.Assessment.Application.Messages
{
    public static class ResponseMessage
    {
        public static string LecturerNotFound => "Lecturer not found!";
        public static string StudentNotFound => "Student not found!";
        public static string PeriodNotFound => "Period not found!";
        public static string CourseNotFound => "Course not found!";
        public static string EmailIsRequired => "Email is required";
        public static string PhoneNumberIsRequired => "Phone number is required";
        public static string PasswordIsRequired => "Password is required";
        public static string FirstnameIsRequired => "Firstname is required";
        public static string CodeIsRequired => "Code is required";
        public static string TitleIsRequired => "Title is required";
        public static string StartDateIsRequired => "Start Date is required";
        public static string EndDateIsRequired => "End Date is required";
        public static string NameIsRequired => "Firstname is required";
        public static string LastnameIsRequired => "Lastname is required";
        public static string DateOfBirthIsRequired => "Date of birth is required";
        public static string StudentIdNotExists => "Student id not exists";
        public static string StudentExists => "Duplicate lecturer by First-name, Last-name, Date-of-Birth.";
        public static string CourseExists => "Duplicate Course by Code";
        public static string PeriodExists => "Duplicate Period by Name";
        public static string LecturerExists => "Duplicate lecturer by First-name, Last-name, Date-of-Birth";
        public static string EmailExists => "Duplicate lecturer by Email address";
        public static string EmailNotValid => "Invalid Email address";
        public static string PhoneNumberNotValid => "Invalid Mobile Number";
    }
}
