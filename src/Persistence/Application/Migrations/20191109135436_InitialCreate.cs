using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Cemiyet.Persistence.Application.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "dimensions",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    creation_date = table.Column<DateTime>(nullable: false),
                    modification_date = table.Column<DateTime>(nullable: false),
                    creator_id = table.Column<Guid>(nullable: false),
                    modifier_id = table.Column<Guid>(nullable: false),
                    width = table.Column<decimal>(type: "numeric(4,2)", nullable: false),
                    height = table.Column<decimal>(type: "numeric(4,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_dimensions", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "genres",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    creation_date = table.Column<DateTime>(nullable: false),
                    modification_date = table.Column<DateTime>(nullable: false),
                    creator_id = table.Column<Guid>(nullable: false),
                    modifier_id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_genres", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "dimensions");

            migrationBuilder.DropTable(
                name: "genres");
        }
    }
}
