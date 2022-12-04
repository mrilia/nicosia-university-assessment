namespace Nicosia.Assessment.Security.Authorization;

public class UserContext : IUserContext
{
    public Guid UserId { get; set; }

    public string Username { get; set; } = null!;

    public Role Role { get; set; }

    public bool IsAuthorized { get; set; }

    public ClaimSet? ClaimSet { get; set; }
}