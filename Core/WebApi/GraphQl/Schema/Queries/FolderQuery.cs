using CloudIn.Core.ApplicationDomain.Contracts.Repositories;
using CloudIn.Core.ApplicationDomain.Entities;

namespace CloudIn.Core.WebApi.GraphQl.Schema.Queries;

[ExtendObjectType(typeof(BaseQuery))]
public class FolderQuery
{
    [UseProjection]
    public IEnumerable<FolderEntity> GetFolders([Service] IFolderRepository folderRepository)
    {
        return folderRepository.GetAll().AsQueryable();
    }
}
