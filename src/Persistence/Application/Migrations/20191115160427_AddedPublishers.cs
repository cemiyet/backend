using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Cemiyet.Persistence.Application.Migrations
{
    public partial class AddedPublishers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "publishers",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    creation_date = table.Column<DateTime>(nullable: false),
                    creator_id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(maxLength: 100, nullable: false),
                    description = table.Column<string>(maxLength: 2000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_publishers", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "publishers");
        }
    }
}
