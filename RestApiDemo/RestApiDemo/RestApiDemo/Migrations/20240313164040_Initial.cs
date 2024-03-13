using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RestApiDemo.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    AuthorId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.AuthorId);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    BookId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false),
                    Genre = table.Column<string>(type: "TEXT", nullable: false),
                    PublishDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    AuthorId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.BookId);
                    table.ForeignKey(
                        name: "FK_Books_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "AuthorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "AuthorId", "Name" },
                values: new object[,]
                {
                    { 1, "Ralls, Kim" },
                    { 2, "Corets, Eva" },
                    { 3, "Randall, Cynthia" },
                    { 4, "Thurman, Paula" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "BookId", "AuthorId", "Description", "Genre", "Price", "PublishDate", "Title" },
                values: new object[,]
                {
                    { 1, 1, "A former architect battles an evil sorceress.", "Fantasy", 14.95m, new DateTime(2000, 12, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Midnight Rain" },
                    { 2, 2, "After the collapse of a nanotechnology society, the youngsurvivors lay the foundation for a new society.", "Fantasy", 12.95m, new DateTime(2000, 11, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Maeve Ascendant" },
                    { 3, 2, "The two daughters of Maeve battle for control of England.", "Fantasy", 12.95m, new DateTime(2001, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Sundered Grail" },
                    { 4, 3, "When Carla meets Paul at an ornithology conference, tempers fly.", "Romance", 7.99m, new DateTime(2000, 9, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lover Birds" },
                    { 5, 4, "A deep sea diver finds true love 20,000 leagues beneath the sea.", "Romance", 6.99m, new DateTime(2000, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Splish Splash" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_AuthorId",
                table: "Books",
                column: "AuthorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Authors");
        }
    }
}
