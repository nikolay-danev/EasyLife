using Microsoft.EntityFrameworkCore.Migrations;

namespace EasyLife.Persistence.Data.Migrations
{
    public partial class DonatorPropertyUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Donators_AspNetUsers_UserId1",
                table: "Donators");

            migrationBuilder.DropIndex(
                name: "IX_Donators_UserId1",
                table: "Donators");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Donators");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Donators");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Donators",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Nickname",
                table: "Donators",
                maxLength: 60,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Donators");

            migrationBuilder.DropColumn(
                name: "Nickname",
                table: "Donators");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Donators",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "Donators",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Donators_UserId1",
                table: "Donators",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Donators_AspNetUsers_UserId1",
                table: "Donators",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
