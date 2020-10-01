using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace pro_API.Migrations
{
    public partial class EditSubtitlesStartandEndtime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "StartTime",
                table: "Subtitles",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "time");

            migrationBuilder.AlterColumn<string>(
                name: "EndtTime",
                table: "Subtitles",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "time");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<TimeSpan>(
                name: "StartTime",
                table: "Subtitles",
                type: "time",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "EndtTime",
                table: "Subtitles",
                type: "time",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
