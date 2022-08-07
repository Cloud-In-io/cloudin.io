using CloudIn.Core.ApplicationDomain.Entities;
using CloudIn.Core.ApplicationDomain.Services.UserService;
using CloudIn.Core.ApplicationDomain.Services.UserService.Interfaces;

namespace CloudIn.Core.ApplicationDomainTests.Services;

[TestClass]
public class UserServiceTests
{
    private readonly IUserService _userService;

    public UserServiceTests()
    {
        _userService = new UserService();
    }

    [TestMethod]
    public async Task Should_Create_An_User()
    {
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
    }
}
