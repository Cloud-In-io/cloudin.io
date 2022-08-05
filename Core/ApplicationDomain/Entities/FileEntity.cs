namespace ApplicationDomain.Entities;

public class FileEntity
{
    public Guid Id { get; set; }

    public string Name { get; set; } = default!;

    public string? MimeType { get; set; }

    public string? PhysicalPath { get; set; }

    public virtual UserEntity OwnerUser { get; set; } = default!;

    public virtual FolderEntity ParentFolder { get; set; } = default!;
}
