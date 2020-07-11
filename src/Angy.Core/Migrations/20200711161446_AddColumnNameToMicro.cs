using Microsoft.EntityFrameworkCore.Migrations;

namespace Angy.Core.Migrations
{
    public partial class AddColumnNameToMicro : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "MicroCategories",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "MicroCategories");
        }
    }
}
