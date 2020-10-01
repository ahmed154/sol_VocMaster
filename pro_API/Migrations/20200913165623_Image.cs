using Microsoft.EntityFrameworkCore.Migrations;

namespace pro_API.Migrations
{
    public partial class Image : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Uri = table.Column<string>(nullable: true),
                    Comment = table.Column<string>(nullable: true),
                    VocId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Images_Vocs_VocId",
                        column: x => x.VocId,
                        principalTable: "Vocs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Images_VocId",
                table: "Images",
                column: "VocId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Images");
        }
    }
}
