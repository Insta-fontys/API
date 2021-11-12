using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccesLibrary.Migrations
{
    public partial class table_changes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Fans");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Fans");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Fans");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Creators");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Creators");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Creators");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Fans",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Fans",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Fans",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Creators",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Creators",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Creators",
                type: "text",
                nullable: true);
        }
    }
}
