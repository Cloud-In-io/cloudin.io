using AutoMapper;
using System.ComponentModel.DataAnnotations;
using CloudIn.Core.ApplicationDomain.Entities;

namespace CloudIn.Core.ApplicationDomain.Services.FileService.Interfaces;

public class IWriteFilePayload
{
    [Required]
    public Guid OwnerUserId { get; set; }

    [Required]
    public Guid ParentFolderId { get; set; }

    [Required]
    public string Name { get; set; } = default!;

    [Required]
    public Stream Content { get; set; } = default!;

    [Required]
    public string MimeType { get; set; } = default!;
}

public class IWriteFilePayloadMapper : Profile
{
    //Source -> Target
    public IWriteFilePayloadMapper()
    {
        CreateMap<IWriteFilePayload, FileEntity>()
            .ForSourceMember(src => src.ParentFolderId, opt => opt.DoNotValidate())
            .ForSourceMember(src => src.Content, opt => opt.DoNotValidate())
            .IgnoreAllPropertiesWithAnInaccessibleSetter();
    }
}
