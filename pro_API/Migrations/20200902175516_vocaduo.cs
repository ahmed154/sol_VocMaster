using Microsoft.EntityFrameworkCore.Migrations;

namespace pro_API.Migrations
{
    public partial class vocaduo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
             name: "VocAudios",
             columns: table => new
             {
                 Id = table.Column<int>(nullable: false)
                     .Annotation("SqlServer:Identity", "1, 1"),
                 Uri = table.Column<string>(nullable: false),
                 Phon = table.Column<string>(nullable: true),
                 VocId = table.Column<int>(nullable: false)
             },
             constraints: table =>
             {
                 table.PrimaryKey("PK_VocAudios", x => x.Id);
             });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
            name: "VocAudios");
        }
    }
}
