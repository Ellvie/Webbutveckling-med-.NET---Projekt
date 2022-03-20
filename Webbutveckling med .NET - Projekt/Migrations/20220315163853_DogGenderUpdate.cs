using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Webbutveckling_med_.NET___Projekt.Migrations
{
    public partial class DogGenderUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "Dog",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Dog");
        }
    }
}
