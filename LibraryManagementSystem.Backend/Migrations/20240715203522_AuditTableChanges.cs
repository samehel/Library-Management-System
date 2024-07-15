using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryManagementSystem.Backend.Migrations
{
    /// <inheritdoc />
    public partial class AuditTableChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ActionType",
                table: "Audits",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "Audits",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "Audits");

            migrationBuilder.AlterColumn<int>(
                name: "ActionType",
                table: "Audits",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);
        }
    }
}
