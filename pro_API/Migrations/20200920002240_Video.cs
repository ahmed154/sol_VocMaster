using Microsoft.EntityFrameworkCore.Migrations;

namespace pro_API.Migrations
{
    public partial class Video : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Videos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(nullable: true),
                    SubtitleId = table.Column<int>(nullable: false),
                    MovieId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Videos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Videos_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Videos_Subtitles_SubtitleId",
                        column: x => x.SubtitleId,
                        principalTable: "Subtitles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Videos_MovieId",
                table: "Videos",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_Videos_SubtitleId",
                table: "Videos",
                column: "SubtitleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Videos");
        }
    }
}
