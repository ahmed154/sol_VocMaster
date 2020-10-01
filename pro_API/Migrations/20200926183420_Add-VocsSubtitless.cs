using Microsoft.EntityFrameworkCore.Migrations;

namespace pro_API.Migrations
{
    public partial class AddVocsSubtitless : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VocsSubtitless",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VocId = table.Column<int>(nullable: false),
                    SubtitleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VocsSubtitless", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VocsSubtitless_Subtitles_SubtitleId",
                        column: x => x.SubtitleId,
                        principalTable: "Subtitles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VocsSubtitless_Vocs_VocId",
                        column: x => x.VocId,
                        principalTable: "Vocs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VocsSubtitless_SubtitleId",
                table: "VocsSubtitless",
                column: "SubtitleId");

            migrationBuilder.CreateIndex(
                name: "IX_VocsSubtitless_VocId",
                table: "VocsSubtitless",
                column: "VocId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VocsSubtitless");
        }
    }
}
