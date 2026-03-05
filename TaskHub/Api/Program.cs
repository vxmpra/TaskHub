using LoggingLibrary;
using Api.DependencyInjection;

namespace Api;

/// <summary>
/// Точка входа приложения
/// </summary>
public sealed class Program
{
    /// <summary>
    /// Запуск приложения
    /// </summary>
    public static void Main(string[] args)
    {
        var host = Host.CreateDefaultBuilder(args)
            .UseInfraSerilog()
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            })
            .Build();

        using (var scope1 = host.Services.CreateScope())
        {
            var provider = scope1.ServiceProvider;

            provider.ResolveAndCompare<SingletonService1>();
            provider.ResolveAndCompare<SingletonService2>();

            provider.ResolveAndCompare<ScopedService1>();
            provider.ResolveAndCompare<ScopedService2>();

            provider.ResolveAndCompare<TransientService1>();
            provider.ResolveAndCompare<TransientService2>();
        }

        using (var scope2 = host.Services.CreateScope())
        {
            var provider = scope2.ServiceProvider;

            provider.ResolveAndCompare<SingletonService1>();
            provider.ResolveAndCompare<SingletonService2>();

            provider.ResolveAndCompare<ScopedService1>();
            provider.ResolveAndCompare<ScopedService2>();

            provider.ResolveAndCompare<TransientService1>();
            provider.ResolveAndCompare<TransientService2>();
        }

        host.Run();
    }
}