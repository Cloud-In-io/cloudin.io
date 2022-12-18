using CloudIn.Core.Domain.Entities;
using CloudIn.Core.Domain.Contracts.Repositories;

namespace CloudIn.Core.Data.Repositories;

public class FileRepository : BaseRepository<FileEntity>, IFileRepository
{
    public FileRepository(DataContext dbContext) : base(dbContext) { }

    public ValueTask DisposeAsync() => _context.DisposeAsync();
}
