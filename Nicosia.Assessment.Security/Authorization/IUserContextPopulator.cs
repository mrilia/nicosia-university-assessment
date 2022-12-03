namespace Nicosia.Assessment.Security.Authorization;

public interface IUserContextPopulator
{
    void Populate(ClaimSet claimSet);
}