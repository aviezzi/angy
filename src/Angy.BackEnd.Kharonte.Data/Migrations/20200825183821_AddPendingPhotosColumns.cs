using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Angy.BackEnd.Kharonte.Data.Migrations
{
    public partial class AddPendingPhotosColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Inserted",
                table: "PendingPhotos");

            migrationBuilder.AddColumn<string>(
                name: "Extension",
                table: "PendingPhotos",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Filename",
                table: "PendingPhotos",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Extension",
                table: "PendingPhotos");

            migrationBuilder.DropColumn(
                name: "Filename",
                table: "PendingPhotos");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "Inserted",
                table: "PendingPhotos",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));
        }
    }
}
