using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Angy.Server.Data.Migrations
{
    public partial class AddMicroCategoryReferenceToItSelf : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "MicroCategoryParentId",
                table: "MicroCategories",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MicroCategories_MicroCategoryParentId",
                table: "MicroCategories",
                column: "MicroCategoryParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_MicroCategories_MicroCategories_MicroCategoryParentId",
                table: "MicroCategories",
                column: "MicroCategoryParentId",
                principalTable: "MicroCategories",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MicroCategories_MicroCategories_MicroCategoryParentId",
                table: "MicroCategories");

            migrationBuilder.DropIndex(
                name: "IX_MicroCategories_MicroCategoryParentId",
                table: "MicroCategories");

            migrationBuilder.DropColumn(
                name: "MicroCategoryParentId",
                table: "MicroCategories");
        }
    }
}
