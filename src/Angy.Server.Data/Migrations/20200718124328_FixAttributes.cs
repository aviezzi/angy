using Microsoft.EntityFrameworkCore.Migrations;

namespace Angy.Server.Data.Migrations
{
    public partial class FixAttributes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttributeDescription_Attribute_AttributeId",
                table: "AttributeDescription");

            migrationBuilder.DropForeignKey(
                name: "FK_AttributeDescription_Products_ProductId",
                table: "AttributeDescription");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AttributeDescription",
                table: "AttributeDescription");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Attribute",
                table: "Attribute");

            migrationBuilder.RenameTable(
                name: "AttributeDescription",
                newName: "AttributeDescriptions");

            migrationBuilder.RenameTable(
                name: "Attribute",
                newName: "Attributes");

            migrationBuilder.RenameIndex(
                name: "IX_AttributeDescription_ProductId",
                table: "AttributeDescriptions",
                newName: "IX_AttributeDescriptions_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_AttributeDescription_AttributeId",
                table: "AttributeDescriptions",
                newName: "IX_AttributeDescriptions_AttributeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AttributeDescriptions",
                table: "AttributeDescriptions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Attributes",
                table: "Attributes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AttributeDescriptions_Attributes_AttributeId",
                table: "AttributeDescriptions",
                column: "AttributeId",
                principalTable: "Attributes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AttributeDescriptions_Products_ProductId",
                table: "AttributeDescriptions",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttributeDescriptions_Attributes_AttributeId",
                table: "AttributeDescriptions");

            migrationBuilder.DropForeignKey(
                name: "FK_AttributeDescriptions_Products_ProductId",
                table: "AttributeDescriptions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Attributes",
                table: "Attributes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AttributeDescriptions",
                table: "AttributeDescriptions");

            migrationBuilder.RenameTable(
                name: "Attributes",
                newName: "Attribute");

            migrationBuilder.RenameTable(
                name: "AttributeDescriptions",
                newName: "AttributeDescription");

            migrationBuilder.RenameIndex(
                name: "IX_AttributeDescriptions_ProductId",
                table: "AttributeDescription",
                newName: "IX_AttributeDescription_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_AttributeDescriptions_AttributeId",
                table: "AttributeDescription",
                newName: "IX_AttributeDescription_AttributeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Attribute",
                table: "Attribute",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AttributeDescription",
                table: "AttributeDescription",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AttributeDescription_Attribute_AttributeId",
                table: "AttributeDescription",
                column: "AttributeId",
                principalTable: "Attribute",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AttributeDescription_Products_ProductId",
                table: "AttributeDescription",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
