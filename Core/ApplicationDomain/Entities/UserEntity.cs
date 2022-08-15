namespace CloudIn.Core.ApplicationDomain.Entities;

public class UserEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid RootFolderId { get; protected set; }

    public string Email { get; set; } = default!;

    public UserName Name { get; set; } = default!;

    public string Password { get; set; } = default!;
}

public class UserName
{
    public string FirstName { get; set; } = default!;

    public string? LastName { get; set; }
}
