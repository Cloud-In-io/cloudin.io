using System.ComponentModel.DataAnnotations;

namespace CloudIn.Core.WebApi.GraphQl.Schema.InputTypes;

public class IPresignedUpload
{
    [Required]
    public Guid UserId { get; set; }

    [Required]
    public Guid FolderId { get; set; }

    [Required]
    public string FileName { get; set; } = default!;
}
