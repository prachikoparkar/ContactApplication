using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using System.Net.Http;
using  Webapi.Contact.Bootstrap;
using Microsoft.AspNetCore.Mvc.Testing;
using Webapi.Contact;
using Autofac;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.IO;

namespace UnitTestContact
{
    public class TestFixture : WebApplicationFactory<Startup>
    {
        public IContainer container;
        public IMediator mediator { get { return Services.GetService<IMediator>(); } }
        public IConfiguration configuration { get { return Services.GetService<IConfiguration>(); } }

        protected override void ConfigureClient(HttpClient client)
        {
            base.ConfigureClient(client);
        }
        protected override IHostBuilder CreateHostBuilder()
        {
            container = AutofacBootstraper.Execute();

            var host = base.CreateHostBuilder()
            .ConfigureWebHostDefaults(webHostBuilder =>
            {
                webHostBuilder
                    .UseContentRoot(Directory.GetCurrentDirectory())
                    .UseStartup<Startup>();
            })
         
            .UseServiceProviderFactory(new AutofacChildLifetimeScopeServiceProviderFactory(container));
            return host;
        }

        //public static Action<IServiceCollection> AddTestAuthentication()
        //{
        //    return services =>
        //    {
        //        services.AddAuthentication("Test")
        //        .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>("Test", options => { });
        //    }
        //}
    }
    //public class TestAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>

    //{
    //    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    //    {
    //        var claims = new[]
    //        {
    //            new ClaimActionCollectionMapExtensions(C)
    //        };
    //    }
    //}
}
