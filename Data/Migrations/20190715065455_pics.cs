using Microsoft.EntityFrameworkCore.Migrations;

namespace Saugatshrestha.Data.Migrations
{
    public partial class pics : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Item",
                table: "products",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "products",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "products");

            migrationBuilder.AlterColumn<string>(
                name: "Item",
                table: "products",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
