using Mc2.CrudTest.Application.Interfaces.messaging;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Infrustructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrustructureLayer(this IServiceCollection services)
        {
            services.AddSingleton<IMessagePublisher, RabbitMqMessagePublisher>();

            return services;
        }
    }
}
