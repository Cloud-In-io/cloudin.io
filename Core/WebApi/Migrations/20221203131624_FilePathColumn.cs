using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CloudIn.Core.WebApi.Migrations
{
    public partial class FilePathColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhysicalPath",
                table: "Files");

            migrationBuilder.AlterColumn<string>(
                name: "MimeType",
                table: "Files",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "Files",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "Files");

            migrationBuilder.AlterColumn<string>(
                name: "MimeType",
                table: "Files",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "PhysicalPath",
                table: "Files",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
