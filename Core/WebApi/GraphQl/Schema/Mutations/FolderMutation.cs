using CloudIn.Core.Domain.Entities;
using CloudIn.Core.Domain.Services.FolderService.Interfaces;

namespace CloudIn.Core.WebApi.GraphQl.Schema.Mutations;

[ExtendObjectType(typeof(BaseMutation))]
public class FolderMutation
{
    public Task<FolderEntity> CreateFolder(
        [Service] IFolderService folderService,
        ICreateFolderPayload createFolderPayload
    ) => folderService.CreateFolderAsync(createFolderPayload);
}
