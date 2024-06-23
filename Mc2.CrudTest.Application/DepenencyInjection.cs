using FluentValidation;
using Mc2.CrudTest.Application.Behaivior;
using Mc2.CrudTest.Application.Customers.Commands;
using Mc2.CrudTest.Application.Interfaces.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Application
{
    public static class DepenencyInjection
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
        {
            services.AddScoped<IValidateService , ValidataService>();
            services.AddScoped<IValidator<CreateCustomerCommand>, CreateCustomerValidator>();
            services.AddScoped<IValidator<UpdateCustomerCommand>, UpdateCustomerValidator>();

            //For Update Validate As same as Create Command ...
            //     services.AddScoped<IValidator<UpdateCustomerCommand>, CreateUpdateCustomerValidator>(); 

            services.AddMediatR(config =>
            {
                config.AddOpenBehavior(typeof(ValidationBehavior<,>));
                config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                config.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            }

        );
            return services;
        }
}
}
