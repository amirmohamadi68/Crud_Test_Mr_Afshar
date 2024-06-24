using FluentAssertions;
using Mc2.CrudTest.Application.Customers.Commands;
using Mc2.CrudTest.Domain.Core;
using Mc2.CrudTest.Domain.Customers;
using MediatR;
using Moq;
using System;
using TechTalk.SpecFlow;
using Xunit;
using static Mc2.CrudTest.Application.Customers.Commands.CreateCustomerCommand;

namespace Mc2.CrudTest.AcceptanceTests
{

    [Binding]
    public class CreateCustomerSteps
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly IMediator _mediator;
        private CustomerDTO _customerDTO;
        private Response _response;

        public CreateCustomerSteps(ScenarioContext scenarioContext, IMediator mediator)
        {
            _scenarioContext = scenarioContext;
            _mediator = mediator;
        }

        [Given(@"a valid customer DTO")]
        public void GivenAValidCustomerDTO()
        {
            _customerDTO = new CustomerDTO
            {
                FirstName = "John",
                LastName = "Doe",
                DateOfBirth = DateTime.Parse("0002-01-01T00:00:00"),
                PhoneNumber = "1234567890",
                Email = "john.doe@example.com",
                BankAccountNumber = "123456789"
            };
            _scenarioContext["CustomerDTO"] = _customerDTO;
        }

        [When(@"the CreateCustomerCommand is handled")]
        public async Task WhenTheCreateCustomerCommandIsHandled()
        {
            _customerDTO = _scenarioContext["CustomerDTO"] as CustomerDTO;

            if (_customerDTO == null)
                throw new ArgumentNullException(nameof(_customerDTO));

            var command = new CreateCustomerCommand { customerDTO = _customerDTO };
            _response = await _mediator.Send(command);
            _scenarioContext["Response"] = _response;
        }

        [Then(@"the customer should be created")]
        public void ThenTheCustomerShouldBeCreated()
        {
            _response = _scenarioContext["Response"] as Response;

            if (_response == null)
                throw new ArgumentNullException(nameof(_response));

            Assert.False(_response.HasError);
            Assert.Equal(201, _response.StatusCode);
            Assert.Equal("Customer Created", _response.ErrorMessage);
        }

        [Then(@"the response should indicate success")]
        public void ThenTheResponseShouldIndicateSuccess()
        {
            _response = _scenarioContext["Response"] as Response;

            if (_response == null)
                throw new ArgumentNullException(nameof(_response));

            Assert.False(_response.HasError);
            Assert.Equal(201, _response.StatusCode);
            Assert.Equal("Customer Created", _response.ErrorMessage);
        }
    }
}