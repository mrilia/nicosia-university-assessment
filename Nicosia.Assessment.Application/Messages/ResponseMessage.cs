
namespace Nicosia.Assessment.Application.Messages
{
    public static class ResponseMessage
    {
        public static string LecturerNotFound => "Lecturer not found!";
        public static string StudentNotFound => "Student not found!";
        public static string StudentIdIsRequired=> "Student id is required";
        public static string PeriodNotFound => "Period not found!";
        public static string SectionNotFound => "Section not found!";
        public static string AdminNotFound => "Admin not found!";
        public static string TokenNotFound => "Token Is Not Active";
        public static string TokenIsNotActive => "Token not found!";
        public static string CourseNotFound => "Course not found!";
        public static string EmailIsRequired => "Email is required";
        public static string UsernameIsRequired => "Username is required";
        public static string IpAdressIsRequired => "IpAdress is required";
        public static string TokenIsRequired => "Token is required";
        public static string SectionIdIsRequired => "Section id is required";
        public static string PhoneNumberIsRequired => "Phone number is required";
        public static string PasswordIsRequired => "Password is required";
        public static string FirstnameIsRequired => "Firstname is required";
        public static string CodeIsRequired => "Code is required";
        public static string TitleIsRequired => "Title is required";
        public static string StartDateIsRequired => "Start Date is required";
        public static string EndDateIsRequired => "End Date is required";
        public static string NameIsRequired => "Firstname is required";
        public static string NumberIsRequired => "Number is required";
        public static string CourseIdIsRequired => "CourseId is required";
        public static string PeriodIdIsRequired => "PeriodId is required";
        public static string LastnameIsRequired => "Lastname is required";
        public static string DateOfBirthIsRequired => "Date of birth is required";
        public static string StudentIdNotExists => "Student id not exists";
        public static string StudentExists => "Duplicate student by First-name, Last-name, Date-of-Birth";
        public static string CourseExists => "Duplicate Course by Code";
        public static string AdminExists => "Duplicate Admin by Code";
        public static string LecturerExists => "Duplicate lecturer by First-name, Last-name, Date-of-Birth";
        public static string EmailExists => "Duplicate by email address";
        public static string EmailNotValid => "Invalid Email address";
        public static string PhoneNumberNotValid => "Invalid Mobile Number";
        public static string UsernamePasswordInvalid => "Invalid Username/Password";
        public static string StudentNotAddedBeforeToClass => "Student not registered before in this class!";
        public static string StudentAddedBeforToClass => "Student is registered in this class already!";
    }
}
