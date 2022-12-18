using CloudIn.Core.Domain.Contracts.Repositories;
using CloudIn.Core.Domain.Entities;

namespace CloudIn.Core.WebApi.GraphQl.Schema.Queries;

[ExtendObjectType(typeof(BaseQuery))]
public class FileQuery
{
    [UseFirstOrDefault]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IEnumerable<FileEntity> GetFile([Service] IFileRepository fileRepository)
    {
        return fileRepository.GetAll().AsQueryable();
    }

    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IEnumerable<FileEntity> GetFiles([Service] IFileRepository fileRepository)
    {
        return fileRepository.GetAll().AsQueryable();
    }
}
