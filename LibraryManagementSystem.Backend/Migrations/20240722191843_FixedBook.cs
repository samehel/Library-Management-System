using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryManagementSystem.Backend.Migrations
{
    /// <inheritdoc />
    public partial class FixedBook : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Remove the CartID column from the Books table
            migrationBuilder.DropColumn(
                name: "CartID",
                table: "Books");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Re-add the CartID column to the Books table
            migrationBuilder.AddColumn<Guid>(
                name: "CartID",
                table: "Books",
                type: "TEXT",
                nullable: true);
        }
    }
}
