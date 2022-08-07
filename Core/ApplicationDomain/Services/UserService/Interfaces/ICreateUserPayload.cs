using System.ComponentModel.DataAnnotations;
using AutoMapper;
using CloudIn.Core.ApplicationDomain.Entities;

namespace CloudIn.Core.ApplicationDomain.Services.UserService.Interfaces;

public class ICreateUserPayload
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = default!;

    [Required]
    [MaxLength(50)]
    public string FirstName { get; set; } = default!;

    [MaxLength(50)]
    public string? LastName { get; set; }

    [Required]
    [MaxLength(50)]
    public string Password { get; set; } = default!;
}

public class ICreateUserPayloadMapper : Profile
{
    //Source -> Target
    public ICreateUserPayloadMapper()
    {
        CreateMap<ICreateUserPayload, UserEntity>()
            .ForMember(dest => dest.Password, opts => opts.Ignore());
    }
}
