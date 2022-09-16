using CloudIn.Core.ApplicationDomain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CloudIn.Core.Data;

public class DataContext : DbContext
{
    public DbSet<UserEntity> Users { get; set; } = null!;

    public DbSet<FileEntity> Files { get; set; } = null!;

    public DbSet<FolderEntity> Folders { get; set; } = null!;

    public DataContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserEntity>().OwnsOne(user => user.Name);

        modelBuilder.Entity<UserEntity>().HasIndex(user => user.Email).IsUnique();

        modelBuilder
            .Entity<UserEntity>()
            .HasOne<FolderEntity>()
            .WithOne()
            .HasForeignKey<UserEntity>(user => user.RootFolderId);

        modelBuilder
            .Entity<FolderEntity>()
            .HasOne<UserEntity>()
            .WithMany()
            .OnDelete(DeleteBehavior.NoAction)
            .HasForeignKey(folder => folder.OwnerUserId)
            .IsRequired();

        modelBuilder
            .Entity<FolderEntity>()
            .HasOne<FolderEntity>()
            .WithMany(f => f.Folders)
            .OnDelete(DeleteBehavior.NoAction)
            .HasForeignKey(folder => folder.ParentFolderId);

        modelBuilder
            .Entity<FileEntity>()
            .HasOne<UserEntity>()
            .WithMany()
            .OnDelete(DeleteBehavior.NoAction)
            .HasForeignKey(file => file.OwnerUserId)
            .IsRequired();
    }
}
