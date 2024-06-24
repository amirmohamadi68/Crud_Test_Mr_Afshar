//using BoDi;
//using FluentValidation;
//using Mc2.CrudTest.Application.Behaivior;
//using Mc2.CrudTest.Application.Customers.Commands;
//using Mc2.CrudTest.Application.Interfaces.messaging;
//using Mc2.CrudTest.Application.Interfaces.Services;
//using Mc2.CrudTest.Domain.Events;
//using Mc2.CrudTest.Domain.Interfaces;
//using Mc2.CrudTest.Infrustructure;
//using Mc2.CrudTest.Persistanse.Context;
//using MediatR;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.DependencyInjection;
//using System;
//using System.Reflection;
//using TechTalk.SpecFlow;
//using TechTalk.SpecFlow.Assist;
//using static Mc2.CrudTest.Application.Customers.Commands.CreateCustomerCommand;
//using Mc2.CrudTest.Application.Customers.Events;
//namespace Mc2.CrudTest.AcceptanceTests.Hooks
//{
//    [Binding]
//    public class SpecFlowHooks
//    {
//        private readonly IObjectContainer _objectContainer;

//        public SpecFlowHooks(IObjectContainer objectContainer)
//        {
//            _objectContainer = objectContainer;
//        }

//        [BeforeScenario]
//        public void RegisterDependencies()
//        {
//            var services = new ServiceCollection();

//            // Register DbContext with in-memory database
//            services.AddDbContext<MyDbContext>(options =>
//                options.UseInMemoryDatabase("TestDatabase"));

//            // Register MediatR and the pipeline behaviors
//            services.AddMediatR(cfg =>
//            {
//                cfg.RegisterServicesFromAssembly(typeof(CreateCustomerCommandHandler).Assembly);
//            });

//            // Register ValidationBehavior in the pipeline
//            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

//            // Register other dependencies
//            services.AddScoped<IDbContext, MyDbContext>();
//            services.AddScoped<IRepository, Repository>();
//            services.AddScoped<IValidateService, ValidataService>();
//            services.AddScoped<IValidator<CreateCustomerCommand>, CreateCustomerValidator>();
//            services.AddScoped<IValidator<UpdateCustomerCommand>, UpdateCustomerValidator>();

//            // Register IMessagePublisher and its implementation
//            services.AddScoped<IMessagePublisher, RabbitMqMessagePublisher>(); // Ensure MessagePublisher is implemented correctly

//            // Register CustomerCreatedEventHandler
//            services.AddScoped<INotificationHandler<CustomerCreatedEvent>, CustomerCreatedEventHandler>();

//            // Build the service provider
//            var serviceProvider = services.BuildServiceProvider();

//            // Register services in the SpecFlow container
//            foreach (var service in services)
//            {
//                _objectContainer.RegisterInstanceAs(serviceProvider.GetService(service.ServiceType), service.ServiceType);
//            }
//        }

//        private void RegisterApplicationLayerDependencies(IServiceCollection services)
//        {
//            // Add registrations for other dependencies in your application layer here
//            // For example:
//            // services.AddScoped<IYourService, YourServiceImplementation>();
//        }
//    }
//}