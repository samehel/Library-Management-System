using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryManagementSystem.Backend.Migrations
{
    /// <inheritdoc />
    public partial class FixedBook2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Drop the foreign key constraint if it exists
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Carts_CartID",
                table: "Books");

            // Drop the index if it exists
            migrationBuilder.DropIndex(
                name: "IX_Books_CartID",
                table: "Books");

            // Drop the CartID column from Books table
            migrationBuilder.DropColumn(
                name: "CartID",
                table: "Books");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Add the CartID column back
            migrationBuilder.AddColumn<Guid>(
                name: "CartID",
                table: "Books",
                type: "TEXT",
                nullable: true);

            // Recreate the index
            migrationBuilder.CreateIndex(
                name: "IX_Books_CartID",
                table: "Books",
                column: "CartID");

            // Recreate the foreign key constraint
            migrationBuilder.AddForeignKey(
                name: "FK_Books_Carts_CartID",
                table: "Books",
                column: "CartID",
                principalTable: "Carts",
                principalColumn: "ID");
        }
    }
}
