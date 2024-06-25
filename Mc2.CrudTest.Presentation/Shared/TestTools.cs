using Mc2.CrudTest.Domain.Customers;
using Mc2.CrudTest.Persistanse.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Presentation.Shared
{
    public class TestTools
    {
        public static MyDbContext? _dbContext;
 



        public static MyDbContext InitializeDBContext()
        {
            var dbContextOptions = new DbContextOptionsBuilder<MyDbContext>()
            .UseInMemoryDatabase("TestDatabase")
            .Options;
            var mockPublisher = new Mock<IPublisher>();

            var dbContext = new MyDbContext(dbContextOptions, mockPublisher.Object);
          
         //   _mockContext = new Mock<MyDbContext>(_dbContext);
            var customers = new List<Customer>
        {
            Customer.Create(new CustomerDTO
            {
                FirstName = "Amir",
                LastName = "Mohamadi",
                DateOfBirth = new DateTime(1990, 1, 1),
                PhoneNumber = "+989362174891",
                Email = "a.mmmmmmm@example.com",
                BankAccountNumber = "12345678"
            }),
            Customer.Create(new CustomerDTO
            {
                FirstName = "Amir",
                LastName = "Amir2",
                DateOfBirth = new DateTime(1992, 2, 2),
                PhoneNumber = "+989362174891",
                Email = "aaaaaaaaa.d@example.com",
                BankAccountNumber = "87654321"
            })
        };

            dbContext.Customers.AddRange(customers);
            dbContext.SaveChanges();
        
            return dbContext;
        }
    }
}
    