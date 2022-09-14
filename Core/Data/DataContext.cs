using CloudIn.Core.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CloudIn.Core.Data;

public class DataContext : DbContext
{
    public DbSet<UserModel> Users { get; set; } = null!;

    public DbSet<FileModel> Files { get; set; } = null!;

    public DbSet<FolderModel> Folders { get; set; } = null!;

    public DataContext(DbContextOptions options) : base(options) { }
}
