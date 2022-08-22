using AutoMapper;
using CloudIn.Core.ApplicationDomain.Contracts.Repositories;
using CloudIn.Core.ApplicationDomain.Entities;
using CloudIn.Core.ApplicationDomain.Services.UserService;
using CloudIn.Core.ApplicationDomainTests.Mocks.Repositories;

namespace CloudIn.Core.ApplicationDomainTests.Services;

[TestClass]
public partial class FolderServiceTests
{
    private readonly IMapper _mapper;

    private readonly MockUserRepository _userRepository;

    private readonly MockFolderRepository _folderRepository;

    private readonly UserService _userService;

    private readonly UserEntity _user;

    public FolderServiceTests()
    {
        var myAssembly = AppDomain.CurrentDomain.GetAssemblies();
        var mapperConfig = new MapperConfiguration(opts => opts.AddMaps(myAssembly));
        _mapper = mapperConfig.CreateMapper();

        _userRepository = new MockUserRepository();
        _folderRepository = new MockFolderRepository();
        _userService = new UserService(_mapper, _userRepository, _folderRepository);

        /* Setup */
        _user = _userService
            .CreateUserAsync(
                new()
                {
                    FirstName = "Foo",
                    LastName = "Bar",
                    Email = "foo@bar.com",
                    Password = "P@s5w0rd123"
                }
            )
            .Result;
    }
}
