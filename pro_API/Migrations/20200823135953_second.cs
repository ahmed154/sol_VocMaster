using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace pro_API.Migrations
{
    public partial class second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

         

           
            migrationBuilder.CreateTable(
                name: "Vocs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vocs", x => x.Id);
                });

           


            migrationBuilder.CreateTable(
                name: "UserInfos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: true),
                    LastVocId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserInfos_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserVocs",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    VocId = table.Column<int>(nullable: false),
                    Id = table.Column<int>(nullable: false),
                    Repetition = table.Column<int>(nullable: false),
                    Success = table.Column<int>(nullable: false),
                    Last1 = table.Column<bool>(nullable: false),
                    Last2 = table.Column<bool>(nullable: false),
                    Last3 = table.Column<bool>(nullable: false),
                    NextReviewTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserVocs", x => new { x.UserId, x.VocId });
                    table.ForeignKey(
                        name: "FK_UserVocs_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserVocs_Vocs_VocId",
                        column: x => x.VocId,
                        principalTable: "Vocs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

       

            migrationBuilder.CreateIndex(
                name: "IX_UserInfos_UserId",
                table: "UserInfos",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserVocs_VocId",
                table: "UserVocs",
                column: "VocId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
           
            migrationBuilder.DropTable(
                name: "UserInfos");

            migrationBuilder.DropTable(
                name: "UserVocs");

        
            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Vocs");
        }
    }
}
