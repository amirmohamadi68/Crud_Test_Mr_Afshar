using Mc2.CrudTest.Application.Customers.Commands;
using Mc2.CrudTest.Domain.Customers;
using Mc2.CrudTest.Domain.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Persistanse.Context
{
    public class MyDbContext : DbContext, IDbContext
    {
        private readonly IPublisher _publisher;

        public MyDbContext(DbContextOptions<MyDbContext> options, IPublisher publisher) : base(options)
        {
            _publisher = publisher;

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().Ignore(c => c.DomainEventsCollection);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MyDbContext).Assembly);

                }

        async Task<int> IDbContext.SaveChangesAsync(CancellationToken cancellationToken)
        {
            int result = 0;
            try
            {
                result = await SaveChangesAsync();
            }
            catch (Exception e)
            {


            }

            var domainEvents = ChangeTracker.Entries<Entity>()
                .Select(e => e.Entity)
                .Where(e => e.DomainEventsCollection.Any())
                .SelectMany(e => e.DomainEventsCollection);

            foreach (var domainEvent in domainEvents)   // i can impiliment outBox event pattern  if you wish 
            {
                // in depp meaning this is not domain event ! Because this message will be publishing out of boundry context , this is intigration event and must be in another name space...
                // but in this case its fine
                //and one thing else , it was better if  i using repository Anti pattern because it was more usefull for Mocking data in test case... thats another case i will handle that some way else
                //  await _publisher.Publish(domainEvent, cancellationToken);
            }
            return result;
        }
       

        public DatabaseFacade datbase => Database;

        public DbSet<Customer> Customers { get; set; }
        //rising event here or in transaction command
        //in here we must checking ChangeTracker to now wich entity has bin changed and values....
        //there is simple better way in mediatR pipeline behavior or use bus in command
        //public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        //{

        //    return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        //}

    }
}
