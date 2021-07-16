using MediatR;
using Microsoft.Extensions.DependencyInjection;
namespace Webapi.Contact.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMediatRServices(this IServiceCollection services)
        {
            services.AddMediatR(typeof(Startup).Assembly);
            return services;

          
        }
    }
}
