using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nicosia.Assessment.Shared.Encryption
{
    public interface IPasswordHasher
    {
        string Hash(string password);
        (bool Verified, bool NeedsUpgrade) Check(string hash, string password);
    }
}
