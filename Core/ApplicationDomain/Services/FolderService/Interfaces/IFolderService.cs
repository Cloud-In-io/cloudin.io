using CloudIn.Core.ApplicationDomain.Entities;

namespace CloudIn.Core.ApplicationDomain.Services.FolderService.Interfaces;

public interface IFolderService
{
    Task<FolderEntity> CreateFolderAsync(ICreateFolderPayload createFolderPayload);
}
