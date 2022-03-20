using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Webbutveckling_med_.NET___Projekt.Migrations
{
    public partial class PersonUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Person",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Reserved",
                table: "Person",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "Reserved",
                table: "Person");
        }
    }
}
