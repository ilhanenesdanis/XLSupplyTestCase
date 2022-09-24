using Microsoft.EntityFrameworkCore.Migrations;

namespace Manager.Migrations
{
    public partial class fixed_v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FilePath",
                table: "MemberFiles",
                newName: "UserName");

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "MemberFiles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FptPassword",
                table: "MemberFiles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FtpUrl",
                table: "MemberFiles",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileName",
                table: "MemberFiles");

            migrationBuilder.DropColumn(
                name: "FptPassword",
                table: "MemberFiles");

            migrationBuilder.DropColumn(
                name: "FtpUrl",
                table: "MemberFiles");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "MemberFiles",
                newName: "FilePath");
        }
    }
}
