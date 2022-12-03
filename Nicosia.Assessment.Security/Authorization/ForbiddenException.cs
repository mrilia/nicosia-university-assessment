using System.Security;

namespace Nicosia.Assessment.Security.Authorization;

[Serializable]
public class ForbiddenException : SecurityException
{
    public ForbiddenException(string message) : base(message)
    {
    }

    public ForbiddenException(string message, Exception innException) : base(message, innException)
    {
    }
}