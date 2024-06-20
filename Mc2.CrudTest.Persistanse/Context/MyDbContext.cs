using Mc2.CrudTest.Application.Customers.Commands;
using Mc2.CrudTest.Domain.Customers;
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
       public MyDbContext()
        {
            
        }
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {


        }
        protected override void OnModelCreating(ModelBuilder modelBuilder) => modelBuilder.ApplyConfigurationsFromAssembly(typeof(MyDbContext).Assembly);

        async Task IDbContext.SaveChanges()
        {
           await SaveChangesAsync();
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
