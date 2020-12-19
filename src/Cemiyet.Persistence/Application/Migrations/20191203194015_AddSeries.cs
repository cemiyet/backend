using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Cemiyet.Persistence.Application.Migrations
{
    public partial class AddSeries : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "series",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    creation_date = table.Column<DateTime>(nullable: false),
                    creator_id = table.Column<Guid>(nullable: false),
                    title = table.Column<string>(maxLength: 100, nullable: false),
                    description = table.Column<string>(maxLength: 2000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_series", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "series_authors",
                columns: table => new
                {
                    series_id = table.Column<Guid>(nullable: false),
                    authors_id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_series_authors", x => new { x.series_id, x.authors_id });
                    table.ForeignKey(
                        name: "FK_series_authors_authors_authors_id",
                        column: x => x.authors_id,
                        principalTable: "authors",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_series_authors_series_series_id",
                        column: x => x.series_id,
                        principalTable: "series",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "series_books",
                columns: table => new
                {
                    series_id = table.Column<Guid>(nullable: false),
                    books_id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_series_books", x => new { x.series_id, x.books_id });
                    table.ForeignKey(
                        name: "FK_series_books_books_books_id",
                        column: x => x.books_id,
                        principalTable: "books",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_series_books_series_series_id",
                        column: x => x.series_id,
                        principalTable: "series",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_series_authors_authors_id",
                table: "series_authors",
                column: "authors_id");

            migrationBuilder.CreateIndex(
                name: "ix_series_books_books_id",
                table: "series_books",
                column: "books_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "series_authors");

            migrationBuilder.DropTable(
                name: "series_books");

            migrationBuilder.DropTable(
                name: "series");
        }
    }
}
