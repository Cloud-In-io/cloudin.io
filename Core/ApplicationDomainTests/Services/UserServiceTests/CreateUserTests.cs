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
}
