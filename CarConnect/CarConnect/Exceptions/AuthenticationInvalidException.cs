using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarConnect.Exceptions
{
    internal class AuthenticationInvalidException : Exception
    {
        public AuthenticationInvalidException() { }

        public AuthenticationInvalidException(string message) : base(message) { }
    }
}
