using System.ComponentModel.DataAnnotations;

namespace CloudIn.Core.WebApi.Common.Contracts;

public class IUploadPayload
{
    [Required]
    public Guid UserId { get; set; }

    [Required]
    public Guid FolderId { get; set; }

    [Required]
    public string FileName { get; set; } = default!;
}