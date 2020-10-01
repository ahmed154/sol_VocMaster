using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace pro_API.Migrations
{
    public partial class syntran : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.CreateTable(
                name: "Synonyms",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(nullable: false),
                    VocId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Synonyms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Synonyms_Vocs_VocId",
                        column: x => x.VocId,
                        principalTable: "Vocs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Translates",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(nullable: false),
                    VocId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Translates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Translates_Vocs_VocId",
                        column: x => x.VocId,
                        principalTable: "Vocs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

      
            migrationBuilder.CreateIndex(
                name: "IX_Synonyms_VocId",
                table: "Synonyms",
                column: "VocId");

            migrationBuilder.CreateIndex(
                name: "IX_Translates_VocId",
                table: "Translates",
                column: "VocId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.DropTable(
                name: "Synonyms");

            migrationBuilder.DropTable(
                name: "Translates");

         
        }
    }
}
