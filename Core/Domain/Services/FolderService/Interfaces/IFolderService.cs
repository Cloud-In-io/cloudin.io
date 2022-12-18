using CloudIn.Core.Domain.Entities;

namespace CloudIn.Core.Domain.Services.FolderService.Interfaces;

public interface IFolderService
{
    Task<FolderEntity> CreateFolderAsync(ICreateFolderPayload createFolderPayload);
}
