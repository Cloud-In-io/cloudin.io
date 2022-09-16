using System.ComponentModel.DataAnnotations;
using CloudIn.Core.ApplicationDomain.Entities;
using CloudIn.Core.ApplicationDomain.Services.UserService;
using CloudIn.Core.ApplicationDomain.Services.UserService.Interfaces;
using CloudIn.Core.ApplicationDomainTests.Mocks.Repositories;

namespace CloudIn.Core.ApplicationDomainTests.Services;

public partial class UserServiceTests
{
    [TestMethod]
    public async Task Should_Create_An_User()
    {
        var userRepository = new MockUserRepository();
        var folderRepository = new MockFolderRepository();
        var _userService = new UserService(_mapper, userRepository, folderRepository);

        ICreateUserPayload createUserPayload =
            new()
            {
                FirstName = "Foo",
                LastName = "Bar",
                Email = "foo@bar.com",
                Password = "P@s5w0rd123"
            };

        var user = await _userService.CreateUserAsync(createUserPayload);

        var rootFolder = folderRepository.Folders.FirstOrDefault();

        Assert.IsNotNull(user);
        Assert.IsNotNull(rootFolder);
        Assert.IsInstanceOfType(user, typeof(UserEntity));

        Assert.AreEqual(createUserPayload.Email, user.Email);
        Assert.AreEqual(rootFolder.Id, user.RootFolderId);
        Assert.AreEqual(rootFolder.Name, $"Root:{user.Id}");

        Assert.AreNotEqual(Guid.Empty, user.Id);
        Assert.AreNotEqual(Guid.Empty, user.RootFolderId);

        Assert.AreEqual(1, userRepository.Users.Count);
        Assert.AreEqual(1, folderRepository.Folders.Count);
    }

    [TestMethod]
    [ExpectedException(typeof(DuplicateWaitObjectException))]
    public async Task Should_Try_Duplicate_An_User()
    {
        var userRepository = new MockUserRepository();
        var folderRepository = new MockFolderRepository();
        var _userService = new UserService(_mapper, userRepository, folderRepository);

        ICreateUserPayload createUserPayload =
            new()
            {
                FirstName = "Foo",
                LastName = "Bar",
                Email = "foo@bar.com",
                Password = "P@s5w0rd123"
            };

        var user = await _userService.CreateUserAsync(createUserPayload);
        var user2 = await _userService.CreateUserAsync(createUserPayload);
    }

    [TestMethod]
    [ExpectedException(typeof(ValidationException))]
    public async Task Should_Try_Create_With_Bad_Data()
    {
        var userRepository = new MockUserRepository();
        var folderRepository = new MockFolderRepository();
        var _userService = new UserService(_mapper, userRepository, folderRepository);

        ICreateUserPayload createUserPayload =
            new()
            {
                LastName = "Bar",
                Email = "foo.com",
                Password = "123"
            };

        var user = await _userService.CreateUserAsync(createUserPayload);
    }
}
