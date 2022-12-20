using CloudIn.Core.Infrastructure.Contracts;

namespace CloudIn.Core.WebApi.Providers;

public static class PluginProviderExtensions
{
    public static IServiceCollection AddPlugins(this IServiceCollection services, string path = @"./plugins/")
    {
        // Load and register the plugins as services
        var pluginProvider = new PluginProvider();
        pluginProvider.LoadPlugins(path, services);

        foreach (var plugin in pluginProvider.GetPlugins())
        {
            Console.WriteLine($"Loading Plugin: {plugin.Name}");
            services.AddSingleton<IPlugin>(plugin);
        }

        return services;
    }

    public static IApplicationBuilder UsePlugins(this IApplicationBuilder app)
    {        
        // Use the service provider to resolve the plugin instances
        var plugins = app.ApplicationServices.GetServices<IPlugin>();

        Console.WriteLine($"Installed plugins: {plugins.Count()}");

        // Configure each plugin
        foreach (var plugin in plugins)
        {
            try
            {
                Console.WriteLine($"Configuring plugin: {plugin.Name}");
                plugin.Configure(app);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine($"Error on loading plugin: {ex.Message}");
            }
        }   

        return app;
    }
}