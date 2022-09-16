using CloudIn.Core.ApplicationDomain.Entities;
using CloudIn.Core.ApplicationDomain.Contracts.Repositories;

namespace CloudIn.Core.Data.Repositories;

public class FolderRepository : BaseRepository<FolderEntity>, IFolderRepository
{
    public FolderRepository(DataContext context) : base(context) { }
}
