using Mc2.CrudTest.Domain.Customers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Collections.Generic;

namespace Mc2.CrudTest.Application.Customers.Commands
{
    public interface IDbContext
    {
        DatabaseFacade Datbase { get; }
        DbSet<Customer> Customers { get; }

    }
}