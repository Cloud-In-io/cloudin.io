using CloudIn.Core.ApplicationDomain.Entities;
using CloudIn.Core.ApplicationDomain.Services.FileService;
using CloudIn.Core.ApplicationDomain.Services.FileService.Interfaces;
using CloudIn.Core.ApplicationDomainTests.Mocks.Providers;
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

        IWriteFilePayload filePayload =
            new()
            {
                Name = "Cat",
                OwnerUserId = _user.Id,
                ParentFolderId = _folder.Id,
                Content = Stream.Null,
                MimeType = "image/png",
            };

        var writtenFile = await fileService.WriteFileAsync(filePayload);

        var expectedPath = $"{_user.Id}/{writtenFile.Id}/file.png";

        Assert.IsInstanceOfType(writtenFile, typeof(FileEntity));
        Assert.IsNotNull(writtenFile);
        Assert.IsNotNull(writtenFile.FilePath);
        Assert.AreEqual(expectedPath, writtenFile.FilePath);
        Assert.AreEqual(filePayload.Name, writtenFile.Name);
        Assert.AreEqual(filePayload.OwnerUserId, writtenFile.OwnerUserId);
        Assert.AreEqual(1, _folder.Files.Count);
        Assert.AreEqual(1, fileRepository.Files.Count);
        Assert.AreSame(_folder, writtenFile.ParentFolder);
    }
}
