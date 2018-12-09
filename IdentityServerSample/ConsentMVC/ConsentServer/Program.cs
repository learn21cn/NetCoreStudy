using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ConsentServer.Data;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ConsentServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //CreateWebHostBuilder(args).
            //    Build().Run();

            BuildWebHost(args)
               .MigrateDbContext<ApplicationDbContext>((context, services) =>
               {
                   new ApplicationDbContextSeed().SeedAsync(context, services)
                   .Wait();
               })
               .Run();
        }

        //原方法
        //public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        //    WebHost.CreateDefaultBuilder(args)
        //        .UseUrls("http://localhost:5000")
        //        .UseStartup<Startup>();



        public static IWebHost BuildWebHost(string[] args) =>
           WebHost.CreateDefaultBuilder(args)
               //.UseEnvironment("Production")
               .UseEnvironment("Development")
               .UseUrls("http://localhost:5000")
               .UseStartup<Startup>()
               .Build();
    }
}
