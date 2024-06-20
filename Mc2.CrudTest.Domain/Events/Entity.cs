using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Domain.Events
{
    public class Entity
    {
        public readonly List<DomainEvent> _domainEvents = new();
        protected void Raise (DomainEvent domainEvent)
        {
            _domainEvents.Add (domainEvent);
        }
    }
}
