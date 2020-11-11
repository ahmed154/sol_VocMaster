using Microsoft.EntityFrameworkCore.Migrations;

namespace pro_API.Migrations
{
    public partial class EditSubtitle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClipUri",
                table: "Subtitles");

            migrationBuilder.DropColumn(
                name: "Index",
                table: "Subtitles");

            migrationBuilder.AlterColumn<decimal>(
                name: "StartTime",
                table: "Subtitles",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<decimal>(
                name: "EndtTime",
                table: "Subtitles",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "StartTime",
                table: "Subtitles",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<string>(
                name: "EndtTime",
                table: "Subtitles",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AddColumn<string>(
                name: "ClipUri",
                table: "Subtitles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Index",
                table: "Subtitles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
