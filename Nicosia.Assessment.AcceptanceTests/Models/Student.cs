﻿namespace Nicosia.Assessment.AcceptanceTests.Models
{
    public class Student
    {
        public ulong Id { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
    }
}