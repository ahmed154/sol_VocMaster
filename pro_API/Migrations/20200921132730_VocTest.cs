using Microsoft.EntityFrameworkCore.Migrations;

namespace pro_API.Migrations
{
    public partial class VocTest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VocTests",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(nullable: true),
                    Freq = table.Column<int>(nullable: false),
                    SubtitleId = table.Column<int>(nullable: false),
                    MovieId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VocTests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VocTests_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_VocTests_Subtitles_SubtitleId",
                        column: x => x.SubtitleId,
                        principalTable: "Subtitles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VocTests_MovieId",
                table: "VocTests",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_VocTests_SubtitleId",
                table: "VocTests",
                column: "SubtitleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VocTests");
        }
    }
}
