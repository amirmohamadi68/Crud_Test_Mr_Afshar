
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mc2.CrudTest.Application;
using Mc2.CrudTest.Application.Customers.Commands;
using static Mc2.CrudTest.Application.Customers.Commands.CreateCustomerCommand;
using Xunit;
using Mc2.CrudTest.Domain.Customers;
using FluentAssertions;


namespace Mc2.CrudTest.AcceptanceTests.Tests
{
    public class CreateCustomerCommandHandlerTests
    {
        private readonly Mock<IDbContext> _dbContextMock;
        private readonly CreateCustomerCommandHandler _handler;

        public CreateCustomerCommandHandlerTests()
        {
            _dbContextMock = new Mock<IDbContext>();
            _handler = new CreateCustomerCommandHandler(_dbContextMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldCreateCustomer()
        {
            // Arrange
            var _customerDTO = new CustomerDTO
            {
                FirstName = "John",
                LastName = "Doe",
                DateOfBirth = new DateTime(1990, 1, 1),
                PhoneNumber = "1234567890",
                Email = "john.doe@example.com",
                BankAccountNumber = "12345678"
            };

            var command = new CreateCustomerCommand { customerDTO = _customerDTO };

            // Act
            var response = await _handler.Handle(command, CancellationToken.None);

            // Assert
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(201);
         

            _dbContextMock.Verify(x => x.Customers.AddAsync(It.IsAny<Customer>(), It.IsAny<CancellationToken>()), Times.Once);
            _dbContextMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
