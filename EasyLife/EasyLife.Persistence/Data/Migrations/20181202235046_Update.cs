using Microsoft.EntityFrameworkCore.Migrations;

namespace EasyLife.Persistence.Data.Migrations
{
    public partial class Update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Creator",
                table: "Advertisements",
                newName: "CreatorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatorId",
                table: "Advertisements",
                newName: "Creator");
        }
    }
}
