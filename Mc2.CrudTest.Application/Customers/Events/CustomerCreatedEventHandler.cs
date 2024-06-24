using Mc2.CrudTest.Application.Interfaces.messaging;
using Mc2.CrudTest.Domain.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Application.Customers.Events
{
    public sealed class CustomerCreatedEventHandler : INotificationHandler<CustomerCreatedEvent>
    {
        private readonly IMessagePublisher _messagePublisher;
        public CustomerCreatedEventHandler(IMessagePublisher messagePublisher)
        {
            _messagePublisher = messagePublisher;
        }
        public async Task Handle(CustomerCreatedEvent notification, CancellationToken cancellationToken)
        { 
            // the best way is to make json string of my object for when i want to consume the message and make json object to recreating db with starting event from first event 

            string message = $"Customer Created _ Fn : {notification.firstName} , LN : {notification.LastName} , Email : {notification.email} , DateOdBirth : {notification.dateOfBirth} , and others.... "; 
            await _messagePublisher.publish(message);
        }
    }
}
