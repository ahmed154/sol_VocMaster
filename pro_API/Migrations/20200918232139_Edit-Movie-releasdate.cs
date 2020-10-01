using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace pro_API.Migrations
{
    public partial class EditMoviereleasdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReleaseDate",
                table: "Movies");

            migrationBuilder.AddColumn<int>(
                name: "ReleaseYear",
                table: "Movies",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReleaseYear",
                table: "Movies");

            migrationBuilder.AddColumn<DateTime>(
                name: "ReleaseDate",
                table: "Movies",
                type: "datetime2",
                nullable: true);
        }
    }
}
