using CloudIn.Core.ApplicationDomain.Contracts.Repositories;
using CloudIn.Core.ApplicationDomain.Entities;

namespace CloudIn.Core.ApplicationDomainTests.Mocks.Repositories;

public class MockFolderRepository : IFolderRepository
{
    public HashSet<FolderEntity> Folders { get; private set; } = new();

    public Task AddAsync(FolderEntity entity)
    {
        Folders.Add(entity);

        return Task.CompletedTask;
    }

    public async Task<IEnumerable<FolderEntity>> GetAllAsync()
    {
        await Task.Delay(1);

        return Folders;
    }

    public async Task<FolderEntity?> GetByIdAsync(Guid Id)
    {
        await Task.Delay(1);

        var Folder = Folders.FirstOrDefault(u => u.Id == Id);

        return Folder;
    }

    public Task RemoveAsync(FolderEntity entity)
    {
        Folders.Remove(entity);

        return Task.CompletedTask;
    }

    public async Task<bool> SaveChangesAsync()
    {
        await Task.Delay(1);

        return true;
    }
}