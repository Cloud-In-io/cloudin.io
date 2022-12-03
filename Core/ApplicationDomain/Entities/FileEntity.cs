namespace CloudIn.Core.ApplicationDomain.Entities;

public class FileEntity : IBaseEntity
{
    public new Guid Id { get; set; } = Guid.NewGuid();

    public Guid OwnerUserId { get; set; } = default!;

    public string Name { get; set; } = default!;

    public string MimeType { get; set; } = default!;

    public string FilePath { get; set; } = default!;

    public virtual FolderEntity ParentFolder { get; protected set; } = default!;

    public void MoveToFolder(FolderEntity folder)
    {
        ParentFolder = folder;
    }
}
