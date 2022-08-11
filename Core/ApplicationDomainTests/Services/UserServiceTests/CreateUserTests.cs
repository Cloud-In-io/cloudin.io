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
        var repository = new MockUserRepository();
        var _userService = new UserService(_mapper, repository);

        ICreateUserPayload createUserPayload =
            new()
            {
                FirstName = "Foo",
                LastName = "Bar",
                Email = "foo@bar.com",
                Password = "P@s5w0rd123"
            };

        var user = await _userService.CreateUserAsync(createUserPayload);

        Assert.IsNotNull(user);
        Assert.IsInstanceOfType(user, typeof(UserEntity));
        Assert.AreEqual(createUserPayload.Email, user.Email);
        Assert.AreEqual(1, repository.Users.Count);
    }

    [TestMethod]
    public async Task Should_Create_An_User_With_Root_Folder()
    {
        var repository = new MockUserRepository();
        var _userService = new UserService(_mapper, repository);

        ICreateUserPayload createUserPayload =
            new()
            {
                FirstName = "Foo",
                LastName = "Bar",
                Email = "foo@bar.com",
                Password = "P@s5w0rd123"
            };

        var user = await _userService.CreateUserAsync(createUserPayload);

        Assert.IsNotNull(user.RootFolder);
        Assert.IsInstanceOfType(user.RootFolder, typeof(FolderEntity));
        Assert.AreEqual(1, user.Folders.Count);
    }

    [TestMethod]
    [ExpectedException(typeof(DuplicateWaitObjectException))]
    public async Task Should_Try_Duplicate_An_User()
    {
        var repository = new MockUserRepository();
        var _userService = new UserService(_mapper, repository);

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
        var repository = new MockUserRepository();
        var _userService = new UserService(_mapper, repository);

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
