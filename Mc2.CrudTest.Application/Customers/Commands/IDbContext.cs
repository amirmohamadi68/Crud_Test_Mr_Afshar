using Mc2.CrudTest.Domain.Customers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Collections.Generic;

namespace Mc2.CrudTest.Application.Customers.Commands
{
    public interface IDbContext
    {
        DatabaseFacade datbase { get; }
        DbSet<Customer> Customers { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
      //  Task<Customer> GetCustomerByIdAsync(CustomerId customerId);
     

    }
}