namespace ApplicationDomain.Entities;

public class FolderEntity
{
    public Guid Id { get; set; }

    public string Name { get; set; } = default!;

    public virtual UserEntity OwnerUser { get; set; } = default!;

    public virtual FolderEntity? ParentFolder { get; set; }

    public virtual ICollection<FileEntity> Files { get; set; } = default!;

    public virtual ICollection<FolderEntity> Folders { get; set; } = default!;
}
