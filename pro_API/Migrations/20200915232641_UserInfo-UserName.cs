using Microsoft.EntityFrameworkCore.Migrations;

namespace pro_API.Migrations
{
    public partial class UserInfoUserName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "UserInfos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserName",
                table: "UserInfos");
        }
    }
}
