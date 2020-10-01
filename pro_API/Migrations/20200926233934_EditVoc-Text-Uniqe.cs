using Microsoft.EntityFrameworkCore.Migrations;

namespace pro_API.Migrations
{
    public partial class EditVocTextUniqe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Text",
                table: "Vocs",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vocs_Text",
                table: "Vocs",
                column: "Text",
                unique: true,
                filter: "[Text] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Vocs_Text",
                table: "Vocs");

            migrationBuilder.AlterColumn<string>(
                name: "Text",
                table: "Vocs",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
