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
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Mc2.CrudTest.Domain.Core;
using System.Text;
using System.Net.Http.Json;
using Castle.Core.Resource;
using Azure.Core;

namespace Mc2.CrudTest.AcceptanceTests
{
    [Binding]
    public class RemoveCustomerStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private WebApplicationFactory<Program> _factory;
        private HttpClient _client;
        public Customer ExsistCustomerDb;
        public RemoveCustomerStepDefinitions(ScenarioContext scenarioContext)
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
            ExsistCustomerDb = context.Customers.FirstOrDefault(w => w.FirstName == "Amir2")!;
        }

        [AfterScenario]
        public void TearDown()
        {
            _client.Dispose();
            _factory.Dispose();
        }
        [Given(@"there is customer in SeedDataBase")]
        public void GivenThereIsCustomerInSeedDataBase()
        {
            ExsistCustomerDb.Id.Should().NotBe(null);
        }

        [When(@"I send a Remove request to Remove the customer")]
        public async Task WhenISendARemoveRequestToRemoveTheCustomer()
        {
            var exsistId = ExsistCustomerDb.Id.CId;
            var response = await _client.DeleteAsync($"/customer/{exsistId}");
            _scenarioContext["Response"] = response;
        }

        [Then(@"Get Result must have a Delleted")]
        public void ThenGetResultMustHaveADelleted()
        {
            var response = _scenarioContext["Response"] as HttpResponseMessage;
            response!.StatusCode.Should().Be(HttpStatusCode.OK);

        }

        [When(@"I send a Remove request with wrong id")]
        public async Task WhenISendARemoveRequestWithWrongId()
        {
           var wrongId = Guid.NewGuid();
            var response = await _client.DeleteAsync($"/customer/{wrongId}");
            _scenarioContext["Response"] = response;

        }

        [Then(@"Get Result must have a Not Found")]
        public void ThenGetResultMustHaveANotFound()
        {
            var a = _scenarioContext["Response"] as HttpResponseMessage;
            a.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
