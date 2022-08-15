using System.ComponentModel.DataAnnotations;
using AutoMapper;
using CloudIn.Core.ApplicationDomain.Contracts.Repositories;
using CloudIn.Core.ApplicationDomain.Entities;
using CloudIn.Core.ApplicationDomain.Services.UserService.Interfaces;

namespace CloudIn.Core.ApplicationDomain.Services.UserService;

public class UserService : IUserService
{
    private readonly IMapper _mapper;

    private readonly IUserRepository _userRepository;

    public UserService(IMapper mapper, IUserRepository userRepository)
    {
        _mapper = mapper;
        _userRepository = userRepository;
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
        var wasSaved = await _userRepository.SaveChangesAsync();

        if (!wasSaved)
        {
            throw new Exception("Could not save this user");
        }

        return user;
    }
}
