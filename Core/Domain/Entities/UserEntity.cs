namespace CloudIn.Core.Domain.Entities;

public class UserEntity : IBaseEntity
{
    public new Guid Id { get; set; } = Guid.NewGuid();

    public Guid? RootFolderId { get; protected set; }

    public string Email { get; set; } = default!;

    public UserName Name { get; set; } = default!;

    public string Password { get; set; } = default!;

    public void SetRootFolder(FolderEntity folder)
    {
        RootFolderId = folder.Id;
    }
}

public class UserName
{
    public string FirstName { get; set; } = default!;

    public string? LastName { get; set; }
}
