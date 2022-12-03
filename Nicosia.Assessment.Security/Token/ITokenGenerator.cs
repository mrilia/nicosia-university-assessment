namespace Nicosia.Assessment.Security.Token;

public interface ITokenGenerator
{
    string GenerateToken(TokenGenerationSettings settings);
}