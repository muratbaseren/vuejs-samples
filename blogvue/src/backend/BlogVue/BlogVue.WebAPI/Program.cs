using BlogVue.WebAPI.DataAccess.Mongo.Context;
using MFramework.Services.DataAccess.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Diagnostics;

namespace BlogVue.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var host = CreateHostBuilder(args).Build();

                using (var scope = host.Services.CreateScope())
                {
                    try
                    {
                        IMongoDBInitializer initializer =
                            scope.ServiceProvider.GetRequiredService<IMongoDBInitializer>();
                        ((IDBInitializer)initializer).Seed();
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("");
                        Debug.WriteLine("Error : " + ex.Message);
                        Debug.WriteLine("");
                    }
                }

                host.Run();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
