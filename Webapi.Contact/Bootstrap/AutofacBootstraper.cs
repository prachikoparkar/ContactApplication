using Autofac;
using Webapi.Contact.IOC;

namespace Webapi.Contact.Bootstrap
{
    public class AutofacBootstraper
    {
        public static IContainer Execute()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<ApiModule>();
            return builder.Build();
        }
    }
}
