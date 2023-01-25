using HotChocolate.AspNetCore.Authorization;
using CloudIn.Core.Domain.Contracts.Repositories;
using CloudIn.Core.Domain.Entities;

namespace CloudIn.Core.WebApi.GraphQl.Schema.Queries;

[ExtendObjectType(typeof(BaseQuery))]
public class FolderQuery
{
    [Authorize]
    [UseFirstOrDefault]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IEnumerable<FolderEntity>? GetFolder([Service] IFolderRepository folderRepository)
    {
        return folderRepository.GetAll().AsQueryable();
    }

    [Authorize]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IEnumerable<FolderEntity>? GetFolders([Service] IFolderRepository folderRepository)
    {
        return folderRepository.GetAll().AsQueryable();
    }
}
