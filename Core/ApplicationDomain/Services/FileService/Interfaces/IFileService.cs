using CloudIn.Core.ApplicationDomain.Entities;

namespace CloudIn.Core.ApplicationDomain.Services.FileService.Interfaces;

public interface IFileService
{
    Task<FileEntity> WriteFileAsync(IWriteFilePayload filePayload);
}
