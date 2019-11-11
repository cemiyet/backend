using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Cemiyet.Persistence.Application.Migrations
{
    public partial class RemovedModificationDateFromEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "modification_date",
                table: "genres");

            migrationBuilder.DropColumn(
                name: "modification_date",
                table: "dimensions");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "modification_date",
                table: "genres",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "modification_date",
                table: "dimensions",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
