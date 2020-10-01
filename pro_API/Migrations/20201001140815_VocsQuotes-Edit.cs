using Microsoft.EntityFrameworkCore.Migrations;

namespace pro_API.Migrations
{
    public partial class VocsQuotesEdit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quote_Influencers_InfluencerId",
                table: "Quote");

            migrationBuilder.DropForeignKey(
                name: "FK_VocsQuotes_Quote_QuoteId",
                table: "VocsQuotes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VocsQuotes",
                table: "VocsQuotes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Quote",
                table: "Quote");

            migrationBuilder.RenameTable(
                name: "Quote",
                newName: "Quotes");

            migrationBuilder.RenameIndex(
                name: "IX_Quote_InfluencerId",
                table: "Quotes",
                newName: "IX_Quotes_InfluencerId");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "VocsQuotes",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VocsQuotes",
                table: "VocsQuotes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Quotes",
                table: "Quotes",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_VocsQuotes_VocId",
                table: "VocsQuotes",
                column: "VocId");

            migrationBuilder.AddForeignKey(
                name: "FK_Quotes_Influencers_InfluencerId",
                table: "Quotes",
                column: "InfluencerId",
                principalTable: "Influencers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VocsQuotes_Quotes_QuoteId",
                table: "VocsQuotes",
                column: "QuoteId",
                principalTable: "Quotes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quotes_Influencers_InfluencerId",
                table: "Quotes");

            migrationBuilder.DropForeignKey(
                name: "FK_VocsQuotes_Quotes_QuoteId",
                table: "VocsQuotes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VocsQuotes",
                table: "VocsQuotes");

            migrationBuilder.DropIndex(
                name: "IX_VocsQuotes_VocId",
                table: "VocsQuotes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Quotes",
                table: "Quotes");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "VocsQuotes");

            migrationBuilder.RenameTable(
                name: "Quotes",
                newName: "Quote");

            migrationBuilder.RenameIndex(
                name: "IX_Quotes_InfluencerId",
                table: "Quote",
                newName: "IX_Quote_InfluencerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VocsQuotes",
                table: "VocsQuotes",
                columns: new[] { "VocId", "QuoteId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Quote",
                table: "Quote",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Quote_Influencers_InfluencerId",
                table: "Quote",
                column: "InfluencerId",
                principalTable: "Influencers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VocsQuotes_Quote_QuoteId",
                table: "VocsQuotes",
                column: "QuoteId",
                principalTable: "Quote",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
