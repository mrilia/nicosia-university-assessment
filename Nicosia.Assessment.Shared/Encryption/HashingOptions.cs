using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nicosia.Assessment.Shared.Encryption
{
    public sealed class HashingOptions
    {
        public int Iterations { get; set; } = 10000;
    }
}
