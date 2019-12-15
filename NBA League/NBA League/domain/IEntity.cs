using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBA_League.domain
{
    interface IEntity<Id>
    {
        Id ID { get; set;}
    }
}
