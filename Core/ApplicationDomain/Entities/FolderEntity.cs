namespace CloudIn.Core.ApplicationDomain.Entities;

public class FolderEntity
{
    public FolderEntity()
    {
        Files = new HashSet<FileEntity>();
        Folders = new HashSet<FolderEntity>();
    }

    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid OwnerUserId { get; protected set; } = default!;

    public Guid? ParentFolderId { get; protected set; }

    public string Name { get; set; } = default!;

    public bool IsRootFolder { get; protected set; } = false;

    public virtual ICollection<FileEntity> Files { get; set; } = default!;

    public virtual ICollection<FolderEntity> Folders { get; set; } = default!;

    public void SetAsRootFolder(UserEntity user)
    {
        OwnerUserId = user.Id;
        ParentFolderId = null;
        IsRootFolder = true;
    }
}
