using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using CloudIn.Core.Infrastructure.Contracts;

namespace MyWebPlugin;

public class MyPlugin : IPlugin
{
    public string Name { get; set; } = nameof(MyWebPlugin);

    public MyPlugin(IServiceCollection services)
    {
        Console.WriteLine($"{Name} - Plugin Setup");
    }

    public void Configure(params object[] parameter)
    {
        var app = parameter.FirstOrDefault(p => p is IApplicationBuilder) as IApplicationBuilder;

        if(app == null)
        {
            throw new ArgumentException($"{Name} Plugin requires an instance of {nameof(IApplicationBuilder)}");
        }

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapGet("/hello", async context =>
            {
                await context.Response.WriteAsync("Hello from MyPlugin!");
            });
        });
    }
}