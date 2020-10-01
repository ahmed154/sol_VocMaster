using Microsoft.EntityFrameworkCore.Migrations;

namespace pro_API.Migrations
{
    public partial class Influencer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Influencers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    ImageUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Influencers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Quote",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(nullable: false),
                    InfluencerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quote", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Quote_Influencers_InfluencerId",
                        column: x => x.InfluencerId,
                        principalTable: "Influencers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Definitions_VocId",
                table: "Definitions",
                column: "VocId");

            migrationBuilder.CreateIndex(
                name: "IX_Quote_InfluencerId",
                table: "Quote",
                column: "InfluencerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Definitions_Vocs_VocId",
                table: "Definitions",
                column: "VocId",
                principalTable: "Vocs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Definitions_Vocs_VocId",
                table: "Definitions");

            migrationBuilder.DropTable(
                name: "Quote");

            migrationBuilder.DropTable(
                name: "Influencers");

            migrationBuilder.DropIndex(
                name: "IX_Definitions_VocId",
                table: "Definitions");
        }
    }
}
