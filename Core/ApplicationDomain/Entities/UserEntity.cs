namespace CloudIn.Core.ApplicationDomain.Entities;

public class UserEntity
{
    public UserEntity()
    {
        Folders = new HashSet<FolderEntity>();
    }

    public Guid Id { get; set; }

    public string Email { get; set; } = default!;

    public string FirstName { get; set; } = default!;

    public string? LastName { get; set; }

    public string Password { get; set; } = default!;

    public virtual FolderEntity RootFolder { get; set; } = default!;

    public virtual ICollection<FolderEntity> Folders { get; set; } = default!;

    public virtual ICollection<FileEntity> Files { get; set; } = default!;
}
