using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace pro_API.Migrations
{
    public partial class Subtitle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Subtitles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Index = table.Column<string>(nullable: false),
                    Text = table.Column<string>(nullable: false),
                    StartTime = table.Column<TimeSpan>(nullable: false),
                    EndtTime = table.Column<TimeSpan>(nullable: false),
                    MovieId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subtitles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subtitles_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Subtitles_MovieId",
                table: "Subtitles",
                column: "MovieId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Subtitles");
        }
    }
}
