using Mc2.CrudTest.Domain.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Domain.Events
{
   public record CustomerCreatedEvent (Guid eventId ,string eventName ,string firstName , string LastName , string email , string phoneNumber , string bankAccountNumber , string dateOfBirth): DomainEvent(eventId);
   public record CustomerUpdatedEvent(Guid eventId,Guid customerId , string eventName, string firstName, string LastName,  string phoneNumber ,string email, string bankAccountNumber, DateTime dateOfBirth) : DomainEvent(eventId);

}
