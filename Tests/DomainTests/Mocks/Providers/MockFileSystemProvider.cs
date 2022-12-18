using CloudIn.Core.Domain.Contracts.Providers.FileSystemProvider;

namespace CloudIn.Core.DomainTests.Mocks.Providers;

public class MockFileSystemProvider : IFileSystemProvider
{
    public Task<FileStream> OpenReadAsync(string filePath)
    {
        throw new NotImplementedException();
    }

    public Task RemoveAsync(string filepath)
    {
        return Task.CompletedTask;
    }

    public async Task<string> WriteAsync(IWriteFileValues fileValues)
    {
        var filepath = $"{fileValues.FilePath}/{fileValues.FileName}/file{fileValues.Extension}";

        await Task.Delay(1);

        return filepath;
    }
}
