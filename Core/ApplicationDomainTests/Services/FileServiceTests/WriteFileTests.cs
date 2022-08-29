using CloudIn.Core.ApplicationDomain.Services.FileService;
using CloudIn.Core.ApplicationDomain.Services.FileService.Interfaces;
using CloudIn.Core.ApplicationDomainTests.Mocks.Repositories;

namespace CloudIn.Core.ApplicationDomainTests.Services;

public partial class FileServiceTests
{
    [TestMethod]
    public async Task Should_Write_A_File()
    {
        var fileRepository = new MockFileRepository();
        var fileSystemProvider = new MockFileSystemProvider();
        var fileService = new FileService(
            _mapper,
            fileSystemProvider,
            fileRepository,
            _folderRepository,
            _userRepository
        );

        ICreateFilePayload filePayload =
            new()
            {
                Name = "Cat",
                OwnerUserId = _user.Id,
                ParentFolderId = _folder.Id,
            };

        var file = await fileService.CreateFileAsync(filePayload);

        IWriteFileContentPayload fileContentPayload =
            new()
            {
                FileId = file.Id,
                Content = Stream.Null,
                MimeType = "image/png",
                OwnerUserId = _user.Id,
            };

        var writtenFile = await fileService.WriteFileContentAsync(fileContentPayload);

        var expectedPath = $"{_user.Id}/{file.Id}/file.png";

        Assert.IsNotNull(writtenFile);
        Assert.IsNotNull(writtenFile.PhysicalPath);
        Assert.AreEqual(file, writtenFile);
        Assert.AreEqual(expectedPath, writtenFile.PhysicalPath);
    }
}
