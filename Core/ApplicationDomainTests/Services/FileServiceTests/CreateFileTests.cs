using System.ComponentModel.DataAnnotations;
using CloudIn.Core.ApplicationDomain.Entities;
using CloudIn.Core.ApplicationDomain.Services.FileService;
using CloudIn.Core.ApplicationDomain.Services.FileService.Interfaces;
using CloudIn.Core.ApplicationDomain.Services.UserService;
using CloudIn.Core.ApplicationDomain.Services.UserService.Interfaces;
using CloudIn.Core.ApplicationDomainTests.Mocks.Repositories;

namespace CloudIn.Core.ApplicationDomainTests.Services;

public partial class FileServiceTests
{
    [TestMethod]
    public async Task Should_Create_A_File()
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

        Assert.IsNotNull(file);
        Assert.IsInstanceOfType(file, typeof(FileEntity));
        Assert.AreEqual(file.Name, file.Name);
        Assert.AreEqual(filePayload.OwnerUserId, file.OwnerUserId);
        Assert.AreSame(_folder, file.ParentFolder);

        Assert.AreEqual(1, _folder.Files.Count);
        Assert.AreEqual(1, fileRepository.Files.Count);
    }
}
