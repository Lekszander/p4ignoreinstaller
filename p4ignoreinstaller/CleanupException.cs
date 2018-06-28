using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace p4ignoreinstaller
{
    class CleanupException : Exception
    {
        public CleanupException()
        {
        }

        public CleanupException(string message)
            : base(message)
        {
        }

        public CleanupException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
