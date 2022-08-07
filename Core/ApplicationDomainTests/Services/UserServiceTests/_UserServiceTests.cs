using AutoMapper;

namespace CloudIn.Core.ApplicationDomainTests.Services;

[TestClass]
public partial class UserServiceTests
{
    private readonly IMapper _mapper;

    public UserServiceTests()
    {
        var myAssembly = AppDomain.CurrentDomain.GetAssemblies();
        var mapperConfig = new MapperConfiguration(opts => opts.AddMaps(myAssembly));
        _mapper = mapperConfig.CreateMapper();
    }
}
