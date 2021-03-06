﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace pro_API.Migrations
{
    public partial class SubtitleRank : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Rank",
                table: "Subtitles",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rank",
                table: "Subtitles");
        }
    }
}
