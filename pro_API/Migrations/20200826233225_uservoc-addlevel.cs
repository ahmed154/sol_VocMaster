using Microsoft.EntityFrameworkCore.Migrations;

namespace pro_API.Migrations
{
    public partial class uservocaddlevel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "UserVocs");

            migrationBuilder.DropColumn(
                name: "Last1",
                table: "UserVocs");

            migrationBuilder.DropColumn(
                name: "Last2",
                table: "UserVocs");

            migrationBuilder.DropColumn(
                name: "Last3",
                table: "UserVocs");

            migrationBuilder.AddColumn<int>(
                name: "Level",
                table: "UserVocs",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "LevelCounter",
                table: "UserVocs",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Level",
                table: "UserVocs");

            migrationBuilder.DropColumn(
                name: "LevelCounter",
                table: "UserVocs");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "UserVocs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Last1",
                table: "UserVocs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Last2",
                table: "UserVocs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Last3",
                table: "UserVocs",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
