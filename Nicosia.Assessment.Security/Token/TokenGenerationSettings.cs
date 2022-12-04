using Nicosia.Assessment.Security.Authorization;

namespace Nicosia.Assessment.Security.Token;

public class TokenGenerationSettings
{
    public Guid UserId { get; set; }
    public string Username { get; set; } = null!;
    public Role Role { get; set; }

    public string SecurityKey { get; set; } = null!;
}