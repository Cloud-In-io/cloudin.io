using CloudIn.Core.Domain.Entities;

namespace CloudIn.Core.Domain.Services.FileService.Interfaces;

public interface IFileService
{
    Task<FileEntity> WriteFileAsync(IWriteFilePayload filePayload);
}
