using Microsoft.Extensions.DependencyInjection;
using CloudIn.Core.Infrastructure.Contracts;
using CloudIn.Core.Domain.Contracts.Providers.FileSystemProvider;
using CloudIn.Plugin.FileSystem.Providers;
using CloudIn.Plugin.FileSystem.Interfaces;
using Microsoft.Extensions.Configuration;

namespace CloudIn.Plugin.FileSystem;

public class FileSystemPlugin : IPlugin
{
    public string Name { get;} = nameof(FileSystemPlugin);

    public FileSystemPlugin(IConfiguration configuration, IServiceCollection services)
    {
        var optionsSection = configuration.GetSection("Plugins:FileSystem");

        services.Configure<FileSystemOptions>(optionsSection);
        services.AddScoped<IFileSystemProvider, FileSystemProvider>();
    }

    public void Configure(params object[] parameters)
    {
        
    }
}
