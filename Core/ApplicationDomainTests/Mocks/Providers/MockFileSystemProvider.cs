using CloudIn.Core.ApplicationDomain.Contracts.Providers.FileSystemProvider;

namespace CloudIn.Core.ApplicationDomainTests.Mocks.Providers;

public class MockFileSystemProvider : IFileSystemProvider
{
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
