using CloudIn.Core.Domain.Contracts.Repositories;
using CloudIn.Core.Domain.Entities;
using HotChocolate.AspNetCore.Authorization;

namespace CloudIn.Core.WebApi.GraphQl.Schema.Queries;

[ExtendObjectType(typeof(BaseQuery))]
public class FileQuery
{
    [Authorize]
    [UseFirstOrDefault]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IEnumerable<FileEntity>? GetFile([Service] IFileRepository fileRepository)
    {
        return fileRepository.GetAll().AsQueryable();
    }

    [Authorize]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IEnumerable<FileEntity>? GetFiles([Service] IFileRepository fileRepository)
    {
        return fileRepository.GetAll().AsQueryable();
    }
}
