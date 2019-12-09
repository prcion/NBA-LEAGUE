using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBA_League.validation
{
    interface IValidator<E>
    {
        void Validate(E e);
    }
}
