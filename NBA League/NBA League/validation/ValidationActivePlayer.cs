using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NBA_League.domain;

namespace NBA_League.validation
{
    class ValidationActivePlayer : IValidator<ActivePlayer>
    {
        public void Validate(ActivePlayer e)
        {
            string err = "";
            if (e.ID == "")
                err += "ID shouldn't be null!\n";
            if (e.numberOfPoints < 0)
                err += "Number of points shouldn't be less than 0!\n";
            if (e.type != "Rezerva" && e.type != "Participant")
                err += "Type should be Rezerva or Participant!\n";
            if (err.Length > 0)
                throw new ValidationException(err);
        }
    }
}
