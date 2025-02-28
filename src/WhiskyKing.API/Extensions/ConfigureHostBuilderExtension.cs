using Autofac;
using Autofac.Extensions.DependencyInjection;
using System.Reflection;
using WhiskyKing.Core.Interfaces;
using WhiskyKing.Infra.Data;

namespace WhiskyKing.API.Extensions;

public static class ConfigureHostBuilderExtension
{
    public static void AddAutofac(this ConfigureHostBuilder host)
    {
        host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

        var webAssembly = Assembly.GetExecutingAssembly();
        var coreAssembly = Assembly.GetAssembly(typeof(IUnitOfWork));
        var infraAssembly = Assembly.GetAssembly(typeof(DatabaseContext));

        host.ConfigureContainer<ContainerBuilder>(builder =>
            builder.RegisterAssemblyTypes(webAssembly, coreAssembly!, infraAssembly!)
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope()
            );
    }
}