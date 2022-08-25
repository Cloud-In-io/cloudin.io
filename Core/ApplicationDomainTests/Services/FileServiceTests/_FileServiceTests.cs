using AutoMapper;
using CloudIn.Core.ApplicationDomain.Entities;
using CloudIn.Core.ApplicationDomain.Services.FolderService;
using CloudIn.Core.ApplicationDomain.Services.UserService;
using CloudIn.Core.ApplicationDomainTests.Mocks.Repositories;

namespace CloudIn.Core.ApplicationDomainTests.Services;

[TestClass]
public partial class FileServiceTests
{
    private readonly IMapper _mapper;

    private readonly MockUserRepository _userRepository;

    private readonly MockFolderRepository _folderRepository;

    private readonly UserEntity _user;

    private readonly FolderEntity _folder;

    public FileServiceTests()
    {
        var myAssembly = AppDomain.CurrentDomain.GetAssemblies();
        var mapperConfig = new MapperConfiguration(opts => opts.AddMaps(myAssembly));
        _mapper = mapperConfig.CreateMapper();

        _userRepository = new MockUserRepository();
        _folderRepository = new MockFolderRepository();

        var userService = new UserService(_mapper, _userRepository, _folderRepository);
        var folderService = new FolderService(_mapper, _folderRepository, _userRepository);

        /* Setup */
        _user = userService
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

        _folder = folderService
            .CreateFolderAsync(
                new()
                {
                    OwnerUserId = _user.Id,
                    ParentFolderId = _user.RootFolderId,
                    Name = "Documents"
                }
            )
            .Result;
    }
}
