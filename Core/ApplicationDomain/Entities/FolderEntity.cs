namespace CloudIn.Core.ApplicationDomain.Entities;

public class FolderEntity
{
    public FolderEntity()
    {
        Files = new HashSet<FileEntity>();
        Folders = new HashSet<FolderEntity>();
    }

    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid OwnerUserId { get; set; } = default!;

    public Guid? ParentFolderId { get; set; }

    public string Name { get; set; } = default!;

    public bool IsRootFolder { get; set; } = false;

    public virtual ICollection<FileEntity> Files { get; set; } = default!;

    public virtual ICollection<FolderEntity> Folders { get; set; } = default!;
}
