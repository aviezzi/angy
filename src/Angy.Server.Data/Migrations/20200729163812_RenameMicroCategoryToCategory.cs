using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Angy.Server.Data.Migrations
{
    public partial class RenameMicroCategoryToCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_MicroCategories_CategoryId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "MicroCategories");

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    ParentCategoryId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_Categories_ParentCategoryId",
                        column: x => x.ParentCategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ParentCategoryId",
                table: "Categories",
                column: "ParentCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.CreateTable(
                name: "MicroCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    MicroCategoryParentId = table.Column<Guid>(type: "uuid", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MicroCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MicroCategories_MicroCategories_MicroCategoryParentId",
                        column: x => x.MicroCategoryParentId,
                        principalTable: "MicroCategories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MicroCategories_MicroCategoryParentId",
                table: "MicroCategories",
                column: "MicroCategoryParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_MicroCategories_CategoryId",
                table: "Products",
                column: "CategoryId",
                principalTable: "MicroCategories",
                principalColumn: "Id");
        }
    }
}
