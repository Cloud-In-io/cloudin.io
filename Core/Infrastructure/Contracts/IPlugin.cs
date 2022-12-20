namespace CloudIn.Core.Infrastructure.Contracts;

public interface IPlugin
{
    string Name { get; }

    void Configure(params object[] parameters);
}