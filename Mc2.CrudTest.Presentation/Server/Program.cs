using Mc2.CrudTest.Application;
using Mc2.CrudTest.Application.Customers.Commands;
using Mc2.CrudTest.Infrustructure;
using Mc2.CrudTest.Persistanse;
using Mc2.CrudTest.Persistanse.Context;
using Mc2.CrudTest.Presentation.Server.Middlware;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Mc2.CrudTest.Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<MyDbContext>(options =>options
                    .UseSqlServer("Server =.; DataBase = Local; UID = sa; PWD = !QAZ2wsx; Trusted_Connection = True; TrustServerCertificate = True") //ConnectionString
                    .EnableSensitiveDataLogging(true));
            // Add services to the container.
            builder.Services.AddInfrustructureLayer();
            builder.Services.AddPersistenceLayer();
            builder.Services.AddApplicationLayer();
            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseWebAssemblyDebugging();
            }
            else
            {
               
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseExceptionHandleMiddleware();
            app.UseHttpsRedirection();

            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();


            app.MapRazorPages();
            app.MapControllers();
            app.MapFallbackToFile("index.html");

            app.Run();
        }
    }
}