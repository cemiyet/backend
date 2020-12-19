using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Cemiyet.Persistence.Application.Migrations
{
    public partial class AddBookModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "books",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    creation_date = table.Column<DateTime>(nullable: false),
                    creator_id = table.Column<Guid>(nullable: false),
                    title = table.Column<string>(maxLength: 100, nullable: false),
                    description = table.Column<string>(maxLength: 2500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_books", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "authors_books",
                columns: table => new
                {
                    authors_id = table.Column<Guid>(nullable: false),
                    books_id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_authors_books", x => new { x.authors_id, x.books_id });
                    table.ForeignKey(
                        name: "FK_authors_books_authors_authors_id",
                        column: x => x.authors_id,
                        principalTable: "authors",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_authors_books_books_books_id",
                        column: x => x.books_id,
                        principalTable: "books",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "book_editions",
                columns: table => new
                {
                    isbn = table.Column<string>(maxLength: 13, nullable: false),
                    page_count = table.Column<short>(nullable: false),
                    print_date = table.Column<DateTime>(nullable: false),
                    creation_date = table.Column<DateTime>(nullable: false),
                    publishers_id = table.Column<Guid>(nullable: false),
                    books_id = table.Column<Guid>(nullable: false),
                    dimensions_id = table.Column<Guid>(nullable: false),
                    creator_id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_book_editions", x => x.isbn);
                    table.ForeignKey(
                        name: "FK_book_editions_books_books_id",
                        column: x => x.books_id,
                        principalTable: "books",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_book_editions_dimensions_dimensions_id",
                        column: x => x.dimensions_id,
                        principalTable: "dimensions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_book_editions_publishers_publishers_id",
                        column: x => x.publishers_id,
                        principalTable: "publishers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "books_genres",
                columns: table => new
                {
                    books_id = table.Column<Guid>(nullable: false),
                    genres_id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_books_genres", x => new { x.books_id, x.genres_id });
                    table.ForeignKey(
                        name: "FK_books_genres_books_books_id",
                        column: x => x.books_id,
                        principalTable: "books",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_books_genres_genres_genres_id",
                        column: x => x.genres_id,
                        principalTable: "genres",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_authors_books_books_id",
                table: "authors_books",
                column: "books_id");

            migrationBuilder.CreateIndex(
                name: "ix_book_editions_books_id",
                table: "book_editions",
                column: "books_id");

            migrationBuilder.CreateIndex(
                name: "ix_book_editions_dimensions_id",
                table: "book_editions",
                column: "dimensions_id");

            migrationBuilder.CreateIndex(
                name: "ix_book_editions_publishers_id",
                table: "book_editions",
                column: "publishers_id");

            migrationBuilder.CreateIndex(
                name: "ix_books_genres_genres_id",
                table: "books_genres",
                column: "genres_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "authors_books");

            migrationBuilder.DropTable(
                name: "book_editions");

            migrationBuilder.DropTable(
                name: "books_genres");

            migrationBuilder.DropTable(
                name: "books");
        }
    }
}
