using Microsoft.EntityFrameworkCore.Migrations;

namespace pro_API.Migrations
{
    public partial class difex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
             migrationBuilder.CreateTable(
             name: "Definitions",
             columns: table => new
             {
                 Id = table.Column<int>(nullable: false)
                     .Annotation("SqlServer:Identity", "1, 1"),
                 Text = table.Column<string>(nullable: false),
                 VocId = table.Column<int>(nullable: false)
             },
             constraints: table =>
             {
                 table.PrimaryKey("PK_Definitions", x => x.Id);
             });

            migrationBuilder.CreateTable(
            name: "Examples",
            columns: table => new
            {
                Id = table.Column<int>(nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Text = table.Column<string>(nullable: false),
                DefinitionId = table.Column<int>(nullable: false),
                VocId = table.Column<int>(nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Examples", x => x.Id);
            });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropTable(
                name: "Examples");

            migrationBuilder.DropTable(
                name: "Definitions");
        }
    }
}
