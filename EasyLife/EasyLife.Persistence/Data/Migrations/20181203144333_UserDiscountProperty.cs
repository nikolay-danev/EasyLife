using Microsoft.EntityFrameworkCore.Migrations;

namespace EasyLife.Persistence.Data.Migrations
{
    public partial class UserDiscountProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Discount",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discount",
                table: "AspNetUsers");
        }
    }
}
