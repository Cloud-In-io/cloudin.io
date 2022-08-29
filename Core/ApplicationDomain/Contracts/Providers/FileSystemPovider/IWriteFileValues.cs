using System.ComponentModel.DataAnnotations;

namespace CloudIn.Core.ApplicationDomain.Contracts.Providers.FileSystemProvider;

public class IWriteFileValues
{
    [Required]
    public Stream Content { get; set; } = default!;

    [Required]
    public string FilePath { get; set; } = default!;

    [Required]
    public string FileName { get; set; } = default!;

    [Required]
    public string Extension { get; set; } = default!;
}
