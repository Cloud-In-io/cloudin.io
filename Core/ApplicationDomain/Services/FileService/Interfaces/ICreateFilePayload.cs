using AutoMapper;
using System.ComponentModel.DataAnnotations;
using CloudIn.Core.ApplicationDomain.Entities;

namespace CloudIn.Core.ApplicationDomain.Services.FileService.Interfaces;

public class ICreateFilePayload
{
    [Required]
    public Guid OwnerUserId { get; set; }

    [Required]
    public Guid ParentFolderId { get; set; }

    [Required]
    public string Name { get; set; } = default!;
}

public class ICreateFilePayloadMapper : Profile
{
    //Source -> Target
    public ICreateFilePayloadMapper()
    {
        CreateMap<ICreateFilePayload, FileEntity>().IgnoreAllPropertiesWithAnInaccessibleSetter();
    }
}
