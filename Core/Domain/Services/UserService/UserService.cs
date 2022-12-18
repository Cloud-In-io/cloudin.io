using System.ComponentModel.DataAnnotations;
using AutoMapper;
using CloudIn.Core.Domain.Contracts.Repositories;
using CloudIn.Core.Domain.Entities;
using CloudIn.Core.Domain.Services.UserService.Interfaces;

namespace CloudIn.Core.Domain.Services.UserService;

public class UserService : IUserService
{
    private readonly IMapper _mapper;

    private readonly IUserRepository _userRepository;

    private readonly IFolderRepository _folderRepository;

    public UserService(
        IMapper mapper,
        IUserRepository userRepository,
        IFolderRepository folderRepository
    )
    {
        _mapper = mapper;
        _userRepository = userRepository;
        _folderRepository = folderRepository;
    }

    public async Task<UserEntity> CreateUserAsync(ICreateUserPayload createUserPayload)
    {
        var hasAlready = await _userRepository.GetUserByEmailAsync(createUserPayload.Email);

        if (hasAlready != null)
        {
            throw new DuplicateWaitObjectException(
                message: "This User's email already exists",
                parameterName: nameof(createUserPayload.Email)
            );
        }

        var ctx = new ValidationContext(createUserPayload);
        Validator.ValidateObject(
            instance: createUserPayload,
            validationContext: ctx,
            validateAllProperties: true
        );

        var user = _mapper.Map<UserEntity>(createUserPayload);
        user.Password = createUserPayload.Password; // Should Encrypt here

        await _userRepository.AddAsync(user);
        var wasUserSaved = await _userRepository.SaveChangesAsync();

        var folder = new FolderEntity();
        await _folderRepository.AddAsync(folder);

        user.SetRootFolder(folder);
        folder.SetAsRootFolder(user);

        var wasFolderSaved = await _folderRepository.SaveChangesAsync();

        if (!wasUserSaved || !wasFolderSaved)
        {
            throw new Exception("Could not save this user");
        }

        return user;
    }
}
