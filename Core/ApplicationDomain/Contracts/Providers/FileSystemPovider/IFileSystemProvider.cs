namespace CloudIn.Core.ApplicationDomain.Contracts.Providers.FileSystemProvider;

public interface IFileSystemProvider
{
    /// <summary>
    /// Writes on file system provider than returns the filepath
    /// </summary>
    /// <param name="content"></param>
    /// <param name="filename"></param>
    /// <returns></returns>
    Task<string> WriteAsync(IWriteFileValues fileValues);

    Task<FileStream> OpenReadAsync(string fileName);

    Task RemoveAsync(string filepath);
}
