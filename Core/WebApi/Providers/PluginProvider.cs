using System.Reflection;
using CloudIn.Core.Infrastructure.Contracts;
public class PluginProvider
{
    private readonly List<IPlugin> _plugins = new List<IPlugin>();

    public void LoadPlugins(string pluginDirectory, IServiceCollection services)
    {
        if (string.IsNullOrEmpty(pluginDirectory))
        {
            throw new ArgumentException("The plugin directory cannot be null or empty.", nameof(pluginDirectory));
        }

        var pluginAssemblies = Directory.GetFiles(pluginDirectory, "*.dll");
        
        foreach (var assemblyPath in pluginAssemblies)
        {
            var assembly = Assembly.LoadFrom(assemblyPath);
            var pluginTypes = assembly.GetTypes().Where(t => t.GetInterfaces().Contains(typeof(IPlugin)));

            foreach (var pluginType in pluginTypes)
            {
                var obj = ActivatorUtilities.CreateInstance(
                        provider: services.BuildServiceProvider(),
                        instanceType: pluginType,
                        services
                    );
                if (obj is IPlugin plugin)
                {
                    _plugins.Add(plugin);
                }
            }
        }
    }

    public IEnumerable<IPlugin> GetPlugins() => _plugins;

    public IPlugin? GetPlugin(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new ArgumentException("The plugin name cannot be null or empty.", nameof(name));
        }

        return _plugins.FirstOrDefault(p => p.Name == name);
    }
}
