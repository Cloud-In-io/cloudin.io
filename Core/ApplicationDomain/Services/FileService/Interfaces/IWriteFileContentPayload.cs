using AutoMapper;
using System.ComponentModel.DataAnnotations;
using CloudIn.Core.ApplicationDomain.Entities;

namespace CloudIn.Core.ApplicationDomain.Services.FileService.Interfaces;

public class IWriteFileContentPayload
{
    [Required]
    public Guid FileId { get; set; } = default!;

    [Required]
    public Stream Content { get; set; } = default!;

    [Required]
    public string MimeType { get; set; } = default!;
}

public class IWriteFileContentPayloadMapper : Profile
{
    //Source -> Target
    public IWriteFileContentPayloadMapper()
    {
        CreateMap<IWriteFileContentPayload, FileEntity>()
            .IgnoreAllPropertiesWithAnInaccessibleSetter();
    }
}
