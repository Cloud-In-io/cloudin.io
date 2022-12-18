using System.ComponentModel.DataAnnotations;
using AutoMapper;
using CloudIn.Core.Domain.Entities;

namespace CloudIn.Core.Domain.Services.FolderService.Interfaces;

public class ICreateFolderPayload
{
    [Required]
    public Guid OwnerUserId { get; set; }

    [Required]
    public Guid ParentFolderId { get; set; }

    [Required]
    public string Name { get; set; } = default!;
}

public class ICreateFolderPayloadMapper : Profile
{
    //Source -> Target
    public ICreateFolderPayloadMapper()
    {
        CreateMap<ICreateFolderPayload, FolderEntity>()
            .IgnoreAllPropertiesWithAnInaccessibleSetter();
    }
}
