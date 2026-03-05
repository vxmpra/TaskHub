namespace Api.DependencyInjection;

public static class ServiceProviderExtensions
{
    public static void ResolveAndCompare<TService>(this IServiceProvider serviceProvider) 
        where TService : class, IHasInstanceId
    {
        var first = serviceProvider.GetService<TService>();
        var second = serviceProvider.GetService<TService>();
        
        Console.WriteLine(typeof(TService).Name);
        Console.WriteLine(first?.InstanceId);
        Console.WriteLine(second?.InstanceId);
        Console.WriteLine(ReferenceEquals(first,second));
    }
}