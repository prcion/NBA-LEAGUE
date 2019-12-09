using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NBA_League.domain;
using NBA_League.validation;

namespace NBA_League.repository
{
    class AbstractRepository<ID, E> : IRepository<ID, E> where E : IEntity<ID>
    {
        IDictionary<ID, E> elems;
        IValidator<E> validator;
        public AbstractRepository(IValidator<E> validator)
        {
            this.validator = validator;
            elems = new Dictionary<ID, E>();
        }

        public virtual E Save(E e)
        {
            try
            {
                validator.Validate(e);
                if (elems.ContainsKey(e.ID))
                {
                    return e;
                }
                elems.Add(e.ID, e);
            }
            catch (ValidationException ex)
            {
                throw ex;
            }
            return default(E);
        }
         
        public virtual E Update(E e)
        {
            try
            {
                validator.Validate(e);
                if (elems.ContainsKey(e.ID))
                {
                    elems[e.ID] = e;
                    return default(E);
                }
            }
            catch (ValidationException ex)
            {
                throw ex;
            }
            return e;
        }

        public virtual E Delete(ID id)
        {
            if(elems.ContainsKey(id))
            {
                E forDelete = elems[id];
                elems.Remove(id);
                return forDelete;
            }
            return default(E);
        }
        
        public E FindOne(ID id)
        {
            if (elems.ContainsKey(id))
                return elems[id];
            return default(E);
        }

        public virtual IEnumerable<E> FindAll()
        {
            return elems.Values;
        }
    }
}
