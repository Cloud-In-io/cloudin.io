﻿// <auto-generated />
using System;
using CloudIn.Core.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CloudIn.Core.WebApi.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20221203131624_FilePathColumn")]
    partial class FilePathColumn
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("CloudIn.Core.Domain.Entities.FileEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FilePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MimeType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("OwnerUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ParentFolderId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("OwnerUserId");

                    b.HasIndex("ParentFolderId");

                    b.ToTable("Files");
                });

            modelBuilder.Entity("CloudIn.Core.Domain.Entities.FolderEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsRootFolder")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("OwnerUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ParentFolderId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("OwnerUserId");

                    b.HasIndex("ParentFolderId");

                    b.ToTable("Folders");
                });

            modelBuilder.Entity("CloudIn.Core.Domain.Entities.UserEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("RootFolderId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("RootFolderId")
                        .IsUnique()
                        .HasFilter("[RootFolderId] IS NOT NULL");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CloudIn.Core.Domain.Entities.FileEntity", b =>
                {
                    b.HasOne("CloudIn.Core.Domain.Entities.UserEntity", null)
                        .WithMany()
                        .HasForeignKey("OwnerUserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("CloudIn.Core.Domain.Entities.FolderEntity", "ParentFolder")
                        .WithMany("Files")
                        .HasForeignKey("ParentFolderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ParentFolder");
                });

            modelBuilder.Entity("CloudIn.Core.Domain.Entities.FolderEntity", b =>
                {
                    b.HasOne("CloudIn.Core.Domain.Entities.UserEntity", null)
                        .WithMany()
                        .HasForeignKey("OwnerUserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("CloudIn.Core.Domain.Entities.FolderEntity", null)
                        .WithMany("Folders")
                        .HasForeignKey("ParentFolderId")
                        .OnDelete(DeleteBehavior.NoAction);
                });

            modelBuilder.Entity("CloudIn.Core.Domain.Entities.UserEntity", b =>
                {
                    b.HasOne("CloudIn.Core.Domain.Entities.FolderEntity", null)
                        .WithOne()
                        .HasForeignKey("CloudIn.Core.Domain.Entities.UserEntity", "RootFolderId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.OwnsOne("CloudIn.Core.Domain.Entities.UserName", "Name", b1 =>
                        {
                            b1.Property<Guid>("UserEntityId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("FirstName")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("LastName")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("UserEntityId");

                            b1.ToTable("Users");

                            b1.WithOwner()
                                .HasForeignKey("UserEntityId");
                        });

                    b.Navigation("Name")
                        .IsRequired();
                });

            modelBuilder.Entity("CloudIn.Core.Domain.Entities.FolderEntity", b =>
                {
                    b.Navigation("Files");

                    b.Navigation("Folders");
                });
#pragma warning restore 612, 618
        }
    }
}
