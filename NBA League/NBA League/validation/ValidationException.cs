using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBA_League.validation
{
    class ValidationException : ApplicationException
    {
        public ValidationException() : base() { }

        public ValidationException(string message) : base(message) { }
    }
}
