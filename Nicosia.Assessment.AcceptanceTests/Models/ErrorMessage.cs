namespace Nicosia.Assessment.AcceptanceTests.Models
{
    public class ErrorMessage
    {
        public ErrorMessage(string code, string description)
        {
            Code = code;
            Description = description;
        }

        public string Code { get; }
        public string Description { get; }
    }
}