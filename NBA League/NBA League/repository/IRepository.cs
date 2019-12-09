using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NBA_League.domain;
namespace NBA_League.repository
{
    interface IRepository<ID, E>
    {
        E Save(E e);
        E Delete(ID id);
        E Update(E e);
        E FindOne(ID id);
        IEnumerable<E> FindAll();
    }
}
