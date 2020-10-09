using Microsoft.EntityFrameworkCore.Migrations;

namespace pro_API.Migrations
{
    public partial class AddPhrasalVerb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PhrasalVerb",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhrasalVerb", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VocsPhrasalVerbs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VocId = table.Column<int>(nullable: false),
                    PhrasalVerbId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VocsPhrasalVerbs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VocsPhrasalVerbs_PhrasalVerb_PhrasalVerbId",
                        column: x => x.PhrasalVerbId,
                        principalTable: "PhrasalVerb",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VocsPhrasalVerbs_Vocs_VocId",
                        column: x => x.VocId,
                        principalTable: "Vocs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PhrasalVerb_Text",
                table: "PhrasalVerb",
                column: "Text",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VocsPhrasalVerbs_PhrasalVerbId",
                table: "VocsPhrasalVerbs",
                column: "PhrasalVerbId");

            migrationBuilder.CreateIndex(
                name: "IX_VocsPhrasalVerbs_VocId",
                table: "VocsPhrasalVerbs",
                column: "VocId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VocsPhrasalVerbs");

            migrationBuilder.DropTable(
                name: "PhrasalVerb");
        }
    }
}
