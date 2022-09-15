using CloudIn.Core.ApplicationDomain.Entities;

namespace CloudIn.Core.Data.Repositories;

public class FileRepository : BaseRepository<FileEntity>
{
    public FileRepository(DataContext context) : base(context) { }
}
