using FluentAssertions;
using Mc2.CrudTest.Domain.Customers;
using Mc2.CrudTest.Persistanse.Context;
using Mc2.CrudTest.Presentation;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Net;
using TechTalk.SpecFlow;
using   Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Mc2.CrudTest.Domain.Core;
using System.Text;

namespace Mc2.CrudTest.AcceptanceTests
{
    [Binding]
    public class CreateCustomerStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private WebApplicationFactory<Program> _factory;
        private HttpClient _client;

        public CreateCustomerStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [BeforeScenario]
        public void Setup()
        {
            _factory = new WebApplicationFactory<Program>()
           .WithWebHostBuilder(builder =>
           {
               builder.ConfigureServices(services =>
               {
                   // Remove the app's MyDbContext registration.
                   var descriptor = services.SingleOrDefault(
                       d => d.ServiceType ==
                           typeof(DbContextOptions<MyDbContext>));

                   if (descriptor != null)
                   {
                       services.Remove(descriptor);
                   }

                   // Add MyDbContext using an in-memory database for testing.
                   services.AddDbContext<MyDbContext>(options =>
                   {
                       options.UseInMemoryDatabase("TestDatabase");
                       options.ConfigureWarnings(w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning));
                   });

                   // Build the service provider.
                   var sp = services.BuildServiceProvider();

                   // Create a scope to obtain a reference to the database
                   // context (MyDbContext).
                   using var scope = sp.CreateScope();
                   var scopedServices = scope.ServiceProvider;
                   var db = scopedServices.GetRequiredService<MyDbContext>();

                   // Ensure the database is created.
                   db.Database.EnsureCreated();

                   // Seed the database with test data.
                   SeedDatabase(db);
               });
           });

            _client = _factory.CreateClient();
        }


        private void SeedDatabase(MyDbContext context)
        {
            var customers = new List<Customer>
            {
                Customer.Create(new CustomerDTO
                {
                    FirstName = "Amir1",
                    LastName = "Mohamadi1",
                    DateOfBirth = new DateTime(1990, 1, 1),
                    PhoneNumber = "+989362174891",
                    Email = "a.mmmmmmm@example.com",
                    BankAccountNumber = "12345678"
                }),
                Customer.Create(new CustomerDTO
                {
                    FirstName = "Amir2",
                    LastName = "Mohamadi2",
                    DateOfBirth = new DateTime(1992, 2, 2),
                    PhoneNumber = "+989362174891",
                    Email = "aaaaaaaaa.d@example.com",
                    BankAccountNumber = "87654321"
                })
            };
            context.Customers.AddRange(customers);
            context.SaveChanges();
        }

        [AfterScenario]
        public void TearDown()
        {
            _client.Dispose();
            _factory.Dispose();
        }

        [Given(@"Create customer information \((.*),(.*),(.*),(.*),(.*),(.*)\)")]
        public void GivenCreateCustomerInformationAmirMohamadi(string firstName ,string lastName, string dateOfBirth, string phoneNumber, string email, string bankAccountNumber)
        {
            var customerDto = new
            {

                FirstName = firstName,
                LastName = lastName,
                DateOfBirth = dateOfBirth,
                PhoneNumber = phoneNumber,
                Email = email,
                BankAccountNumber = bankAccountNumber
            };

            _scenarioContext["CustomerDto"] = customerDto;
        }

        [When(@"I send a POST request to create the customer")]
        public async Task WhenISendAPostRequestToCreateTheCustomer()
        {
            var customerDto = _scenarioContext["CustomerDto"];
            var content = new StringContent(JsonConvert.SerializeObject(customerDto), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/customer", content);

            _scenarioContext["Response"] = response;
        }

        [Then(@"Create result should be succeeded")]
        public async Task ThenCreateResultShouldBeSucceeded()
        {
            var response = _scenarioContext["Response"] as HttpResponseMessage;
            response!.StatusCode.Should().Be(HttpStatusCode.OK);

        }

        [Then(@"Create result should be failed")]
        public async Task ThenCreateResultShouldBeFailed()
        {
            var response = _scenarioContext["Response"] as HttpResponseMessage;
            response!.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var responseContent = await response.Content.ReadAsStringAsync();
            //var controllerResponse = JsonConvert.DeserializeObject<Response>(responseContent);

           // controllerResponse.HasError.Should().BeTrue();
           // controllerResponse.ErrorMessage.Should().Contain("Email must be unique in the database");
        }
    }
}
