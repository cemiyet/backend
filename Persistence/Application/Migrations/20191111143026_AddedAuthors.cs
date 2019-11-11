using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Cemiyet.Persistence.Application.Migrations
{
    public partial class AddedAuthors : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "authors",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    creation_date = table.Column<DateTime>(nullable: false),
                    creator_id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(maxLength: 25, nullable: false),
                    surname = table.Column<string>(maxLength: 25, nullable: false),
                    bio = table.Column<string>(maxLength: 2000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_authors", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "authors");
        }
    }
}
