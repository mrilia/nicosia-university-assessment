namespace Nicosia.Assessment.Security.Cryptography;

public interface IHashProvider
{
    string GetSaltedHash(string value, string salt);
}