using CloudIn.Core.Domain.Contracts.Providers.FileSystemProvider;
using Path = System.IO.Path;

namespace CloudIn.Core.WebApi.Providers;

public class FileSystemProvider : IFileSystemProvider, IAsyncDisposable
{
    private readonly DirectoryInfo _rootPath;

    private FileStream FileStream { get; set; } = default!;

    public FileSystemProvider(string rootPath)
    {
        _rootPath = new DirectoryInfo(rootPath);
        if (!_rootPath.Exists)
            _rootPath.Create();
    }

    public Task RemoveAsync(string filepath)
    {
        var fileInfo = new FileInfo(filepath);

        if (!fileInfo.Exists)
        {
            throw new FileNotFoundException("The file has already been removed.");
        }

        fileInfo.Delete();

        return Task.CompletedTask;
    }

    public async Task<string> WriteAsync(IWriteFileValues fileValues)
    {
        var directory =
            _rootPath.EnumerateDirectories(fileValues.FilePath).FirstOrDefault()
            ?? _rootPath.CreateSubdirectory(fileValues.FilePath);

        if (directory == null)
            throw new DirectoryNotFoundException();

        var fileFolder =
            directory.EnumerateDirectories(fileValues.FileName).FirstOrDefault()
            ?? directory.CreateSubdirectory(fileValues.FileName);

        var filePath = Path.Combine(fileFolder.FullName, "file");
        var fileName = Path.ChangeExtension(filePath, fileValues.Extension);

        var file = new FileInfo(fileName);

        FileStream = file.Create();

        await fileValues.Content.CopyToAsync(FileStream);

        await FileStream.FlushAsync();

        var virtualPath = Path.GetRelativePath(_rootPath.FullName, file.FullName);

        return virtualPath;
    }

    public async Task<FileStream> OpenReadAsync(string virtualPath)
    {
        await Task.Yield();

        var physicalPath = Path.Combine(_rootPath.FullName, virtualPath);

        var fileInfo = new FileInfo(physicalPath);

        if (!fileInfo.Exists)
            throw new FileNotFoundException();

        FileStream = fileInfo.OpenRead();

        return FileStream;
    }

    ValueTask IAsyncDisposable.DisposeAsync()
    {
        return FileStream.DisposeAsync();
    }
}
