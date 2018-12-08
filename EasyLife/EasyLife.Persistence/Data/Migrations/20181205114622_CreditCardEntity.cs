using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EasyLife.Persistence.Data.Migrations
{
    public partial class CreditCardEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreditCardId",
                table: "Donators",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CreditCards",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DonatorId = table.Column<int>(nullable: false),
                    CardNumber = table.Column<decimal>(maxLength: 19, nullable: false),
                    CardOwnerName = table.Column<string>(maxLength: 20, nullable: false),
                    ExpiryDate = table.Column<DateTime>(nullable: false),
                    CCVNumber = table.Column<int>(maxLength: 3, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditCards", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Donators_CreditCardId",
                table: "Donators",
                column: "CreditCardId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Donators_CreditCards_CreditCardId",
                table: "Donators",
                column: "CreditCardId",
                principalTable: "CreditCards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Donators_CreditCards_CreditCardId",
                table: "Donators");

            migrationBuilder.DropTable(
                name: "CreditCards");

            migrationBuilder.DropIndex(
                name: "IX_Donators_CreditCardId",
                table: "Donators");

            migrationBuilder.DropColumn(
                name: "CreditCardId",
                table: "Donators");
        }
    }
}
