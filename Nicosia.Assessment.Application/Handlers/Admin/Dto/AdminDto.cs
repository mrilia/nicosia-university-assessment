using System;

namespace Nicosia.Assessment.Application.Handlers.Admin.Dto
{
    public class AdminDto
    {
        public Guid AdminId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}
