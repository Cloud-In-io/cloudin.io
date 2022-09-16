using CloudIn.Core.ApplicationDomain.Contracts.Repositories;
using CloudIn.Core.ApplicationDomain.Entities;

namespace CloudIn.Core.WebApi.GraphQl.Schema.Queries;

[ExtendObjectType(typeof(BaseQuery))]
public class FileQuery
{
    [UseProjection]
    public IEnumerable<FileEntity> GetFiles([Service] IFileRepository fileRepository)
    {
        return fileRepository.GetAll().AsQueryable();
    }
}
