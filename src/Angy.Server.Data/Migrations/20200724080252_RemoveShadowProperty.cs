using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Angy.Server.Data.Migrations
{
    public partial class RemoveShadowProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_MicroCategories_MicroCategoryId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_MicroCategoryId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "MicroCategoryId",
                table: "Products");

            migrationBuilder.AddColumn<Guid>(
                name: "CategoryId",
                table: "Products",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_MicroCategories_CategoryId",
                table: "Products",
                column: "CategoryId",
                principalTable: "MicroCategories",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_MicroCategories_CategoryId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_CategoryId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Products");

            migrationBuilder.AddColumn<Guid>(
                name: "MicroCategoryId",
                table: "Products",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_MicroCategoryId",
                table: "Products",
                column: "MicroCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_MicroCategories_MicroCategoryId",
                table: "Products",
                column: "MicroCategoryId",
                principalTable: "MicroCategories",
                principalColumn: "Id");
        }
    }
}
