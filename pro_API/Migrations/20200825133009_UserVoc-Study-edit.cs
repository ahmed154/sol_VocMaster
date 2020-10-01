using Microsoft.EntityFrameworkCore.Migrations;

namespace pro_API.Migrations
{
    public partial class UserVocStudyedit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Stydy",
                table: "UserVocs");

            migrationBuilder.AddColumn<bool>(
                name: "Study",
                table: "UserVocs",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Study",
                table: "UserVocs");

            migrationBuilder.AddColumn<bool>(
                name: "Stydy",
                table: "UserVocs",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
