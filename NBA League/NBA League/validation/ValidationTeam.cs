using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NBA_League.domain;

namespace NBA_League.validation
{
    class ValidationTeam : IValidator<Team>
    {
        public void Validate(Team e)
        {
            string err = "";
            if (e.ID < 0)
                err += "ID shouldn't be less than 0!\n";
            if (e.name == "")
                err += "Name shouldn't be null!\n";
            if (err.Length > 0)
                throw new ValidationException(err);
        }
    }
}
