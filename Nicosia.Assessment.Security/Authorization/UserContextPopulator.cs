namespace Nicosia.Assessment.Security.Authorization;

public class UserContextPopulator : IUserContextPopulator
{
    private readonly IUserContext context;


    public UserContextPopulator(IUserContext userContext)
    {
        context = userContext;
    }

    public void Populate(ClaimSet claimSet)
    {
        if (context is UserContext userContext)
        {
            var userId = claimSet[ClaimType.UserId];
            userContext.UserId = Guid.Parse(userId);

            userContext.Username = claimSet[ClaimType.Username];
            
            var role = claimSet[ClaimType.Role];
            userContext.Role = Enum.Parse<Role>(role);

            userContext.IsAuthorized = true;

            userContext.ClaimSet = claimSet;
        }
    }
}