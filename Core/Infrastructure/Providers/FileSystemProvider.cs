using CloudIn.Core.ApplicationDomain.Contracts.Providers.FileSystemProvider;

namespace CloudIn.Core.Infrastructure.Providers;

public class FileSystemProvider : IFileSystemProvider, IAsyncDisposable
{
    private readonly DirectoryInfo _rootPath;

    private FileStream FileStream { get; set; } = default!;

    public FileSystemProvider(string rootPath)
    {
        _rootPath = new DirectoryInfo(rootPath);
        if(!_rootPath.Exists) _rootPath.Create();
    }

    public Task RemoveAsync(string filepath)
    {
        var fileInfo = new FileInfo(filepath);

        if(!fileInfo.Exists)
        {
            throw new FileNotFoundException("The file has already been removed.");
        }

        fileInfo.Delete();

        return Task.CompletedTask;
    }

    public async Task<string> WriteAsync(IWriteFileValues fileValues)
    {
        var directory = _rootPath.EnumerateDirectories(fileValues.FilePath).FirstOrDefault() ?? _rootPath.CreateSubdirectory(fileValues.FilePath);

        if(directory == null)
            throw new DirectoryNotFoundException();

        var path = Path.Combine(directory.FullName, fileValues.FileName);
        var fileName = Path.ChangeExtension(path, fileValues.Extension);

        var fileInfo = new FileInfo(fileName);

        FileStream = fileInfo.Create();

        await fileValues.Content.CopyToAsync(FileStream);

        await FileStream.FlushAsync();

        return fileInfo.FullName;
    }

    ValueTask IAsyncDisposable.DisposeAsync() => FileStream.DisposeAsync();
}