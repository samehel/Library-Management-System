using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryManagementSystem.Backend.Migrations
{
    /// <inheritdoc />
    public partial class AddM2MRelationship_BooksCart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Carts_CartID",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_CartID",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "CartID",
                table: "Books");

            migrationBuilder.CreateTable(
                name: "CartBooks",
                columns: table => new
                {
                    CartID = table.Column<Guid>(type: "TEXT", nullable: false),
                    BookID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartBooks", x => new { x.CartID, x.BookID });
                    table.ForeignKey(
                        name: "FK_CartBooks_Books_BookID",
                        column: x => x.BookID,
                        principalTable: "Books",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartBooks_Carts_CartID",
                        column: x => x.CartID,
                        principalTable: "Carts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartBooks_BookID",
                table: "CartBooks",
                column: "BookID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartBooks");

            migrationBuilder.AddColumn<Guid>(
                name: "CartID",
                table: "Books",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Books_CartID",
                table: "Books",
                column: "CartID");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Carts_CartID",
                table: "Books",
                column: "CartID",
                principalTable: "Carts",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
