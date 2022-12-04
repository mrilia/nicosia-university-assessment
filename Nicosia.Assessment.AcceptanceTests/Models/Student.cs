namespace Nicosia.Assessment.AcceptanceTests.Models
{
    public class Student
    {
        public Guid StudentId { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string? DateOfBirth { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
    }
}