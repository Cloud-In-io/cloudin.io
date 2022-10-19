using CloudIn.Core.ApplicationDomain.Entities;
using CloudIn.Core.ApplicationDomain.Services.FolderService.Interfaces;

namespace CloudIn.Core.WebApi.GraphQl.Schema.Mutations;

[ExtendObjectType(typeof(BaseMutation))]
public class FolderMutation
{
    public Task<FolderEntity> CreateFolder(
        [Service] IFolderService folderService,
        ICreateFolderPayload createFolderPayload
    ) => folderService.CreateFolderAsync(createFolderPayload);
}
