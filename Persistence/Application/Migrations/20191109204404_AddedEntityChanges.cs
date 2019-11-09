using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Cemiyet.Persistence.Application.Migrations
{
    public partial class AddedEntityChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "modifier_id",
                table: "genres");

            migrationBuilder.DropColumn(
                name: "modifier_id",
                table: "dimensions");

            migrationBuilder.CreateTable(
                name: "entity_changes",
                columns: table => new
                {
                    entity_id = table.Column<Guid>(nullable: false),
                    modifier_id = table.Column<Guid>(nullable: false),
                    property_name = table.Column<string>(nullable: false),
                    old_value = table.Column<string>(nullable: false),
                    new_value = table.Column<string>(nullable: false),
                    modification_date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateIndex(
                name: "ix_entity_changes_entity_id",
                table: "entity_changes",
                column: "entity_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "entity_changes");

            migrationBuilder.AddColumn<Guid>(
                name: "modifier_id",
                table: "genres",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "modifier_id",
                table: "dimensions",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
