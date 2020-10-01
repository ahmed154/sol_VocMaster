using Microsoft.EntityFrameworkCore.Migrations;

namespace pro_API.Migrations
{
    public partial class VocsQuotes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VocsQuotes",
                columns: table => new
                {
                    VocId = table.Column<int>(nullable: false),
                    QuoteId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VocsQuotes", x => new { x.VocId, x.QuoteId });
                    table.ForeignKey(
                        name: "FK_VocsQuotes_Quote_QuoteId",
                        column: x => x.QuoteId,
                        principalTable: "Quote",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VocsQuotes_Vocs_VocId",
                        column: x => x.VocId,
                        principalTable: "Vocs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VocsQuotes_QuoteId",
                table: "VocsQuotes",
                column: "QuoteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VocsQuotes");
        }
    }
}
