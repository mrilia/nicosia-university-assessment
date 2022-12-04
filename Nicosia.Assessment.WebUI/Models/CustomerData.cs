using System;
using System.ComponentModel.DataAnnotations;

namespace Nicosia.Assessment.Presentation.Front.Models
{
    public class StudentData
    {
        public long Id { get; set; }

        [Required]
        public string Firstname { get; set; }

        [Required]
        public string Lastname { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; }

        [Required]
        public string BankAccountNumber { get; set; }
    }
}