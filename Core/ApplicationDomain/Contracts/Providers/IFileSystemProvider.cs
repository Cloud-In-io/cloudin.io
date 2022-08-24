namespace CloudIn.Core.ApplicationDomain.Contracts.Providers;

public interface IFileSystemProvider
{
    /// <summary>
    /// Writes on file system provider than returns the filepath
    /// </summary>
    /// <param name="content"></param>
    /// <returns></returns>
    Task<string> WriteAsync(Stream content);

    Task RemoveAsync(string filepath);
}
