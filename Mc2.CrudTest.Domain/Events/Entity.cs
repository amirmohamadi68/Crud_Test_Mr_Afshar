using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Domain.Events
{
    public abstract class Entity
    {
        private readonly List<DomainEvent> _domainEvents = new();
        public ICollection<DomainEvent> DomainEventsCollection=> _domainEvents;
        protected void Raise (DomainEvent domainEvent)
        {
            _domainEvents.Add (domainEvent);
        }
    }
}
