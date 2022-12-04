namespace Nicosia.Assessment.AcceptanceTests.Models
{
    public class Student
    {
        public Guid StudentId { get; set; }
        public string Firstname { get; set; } = null!;
        public string Lastname { get; set; } = null!;
        public DateOnly DateOfBirth { get; set; }
        public string PhoneNumber { get; set; } = null!;
        public string Email { get; set; } = null!;
    }
}