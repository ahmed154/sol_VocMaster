using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace pro_API.Migrations
{
    public partial class Movie : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Text = table.Column<string>(nullable: false),
                    ReleaseDate = table.Column<DateTime>(nullable: true),
                    PosterUri = table.Column<string>(nullable: true),
                    TrailerUri = table.Column<string>(nullable: true),
                    Summary = table.Column<string>(nullable: true),
                    LangId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Movies_Langs_LangId",
                        column: x => x.LangId,
                        principalTable: "Langs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Movies_LangId",
                table: "Movies",
                column: "LangId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Movies");
        }
    }
}
