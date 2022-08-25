using CloudIn.Core.ApplicationDomain.Contracts.Repositories;
using CloudIn.Core.ApplicationDomain.Entities;

namespace CloudIn.Core.ApplicationDomainTests.Mocks.Repositories;

public class MockFileRepository : IFileRepository
{
    public HashSet<FileEntity> Files { get; private set; } = new();

    public Task AddAsync(FileEntity entity)
    {
        Files.Add(entity);

        return Task.CompletedTask;
    }

    public async Task<IEnumerable<FileEntity>> GetAllAsync()
    {
        await Task.Delay(1);

        return Files;
    }

    public async Task<FileEntity?> GetByIdAsync(Guid Id)
    {
        await Task.Delay(1);

        var File = Files.FirstOrDefault(u => u.Id == Id);

        return File;
    }

    public Task RemoveAsync(FileEntity entity)
    {
        Files.Remove(entity);

        return Task.CompletedTask;
    }

    public async Task<bool> SaveChangesAsync()
    {
        await Task.Delay(1);

        return true;
    }
}
