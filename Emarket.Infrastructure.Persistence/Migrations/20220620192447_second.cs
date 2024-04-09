using Microsoft.EntityFrameworkCore.Migrations;

namespace Emarket.Infrastructure.Persistence.Migrations
{
    public partial class second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "Adds",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Image2",
                table: "Adds",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image3",
                table: "Adds",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image4",
                table: "Adds",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image2",
                table: "Adds");

            migrationBuilder.DropColumn(
                name: "Image3",
                table: "Adds");

            migrationBuilder.DropColumn(
                name: "Image4",
                table: "Adds");

            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "Adds",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
