using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webapi.Contact.Bootstrap;
using System.IO;
using Autofac.Extensions.DependencyInjection;
namespace Webapi.Contact
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {

            var container = AutofacBootstraper.Execute();
            return new HostBuilder()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .ConfigureHostConfiguration(ConfigureHost(args))
                .UseServiceProviderFactory(new AutofacChildLifetimeScopeServiceProviderFactory(container))
                .ConfigureWebHostDefaults(ConfigureWebHost());
           
        }
        public static Action<IWebHostBuilder> ConfigureWebHost()
        {
            return webBuilder =>
            {
                webBuilder.UseStartup<Startup>();

            };
        }
        public static Action<IConfigurationBuilder> ConfigureHost(string[] args)
        {
            return config =>
            {
                if (args != null)
                    config.AddCommandLine(args);
            };
        }
    }
}
