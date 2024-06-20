using Mc2.CrudTest.Domain.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Domain.Events
{
    public record  CustomerRemovedEvent(Guid Id , CustomerId CustomerId) : DomainEvent(Id);
  
}
