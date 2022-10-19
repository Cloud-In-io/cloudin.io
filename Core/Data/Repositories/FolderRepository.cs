using CloudIn.Core.ApplicationDomain.Entities;
using CloudIn.Core.ApplicationDomain.Contracts.Repositories;

namespace CloudIn.Core.Data.Repositories;

public class FolderRepository : BaseRepository<FolderEntity>, IFolderRepository, IAsyncDisposable
{
    public FolderRepository(DataContext dbContext) : base(dbContext) { }

    public ValueTask DisposeAsync() => _context.DisposeAsync();
}
