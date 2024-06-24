using Mc2.CrudTest.Application.Customers.Commands;
using Mc2.CrudTest.Domain.Interfaces;
using Mc2.CrudTest.Persistanse.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Mc2.CrudTest.Persistanse
{
    public static class DependencyInjection
    {

        private const string ConnectionString = "Server =.; DataBase = Local; UID = sa; PWD = !QAZ2wsx; Trusted_Connection = True; TrustServerCertificate = True"; 
        // i will move that to appzetting and IOption at the end
        public static IServiceCollection AddPersistenceLayer(this IServiceCollection services)
        {
            services.AddDbContext<MyDbContext>(option =>
                             option.UseSqlServer(ConnectionString));
            services.AddScoped<IDbContext, MyDbContext>();
            services.AddScoped<IRepository, Repository>();
            return services;
        }
    }
}
