using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BooksAPI.Migrations.LibraryMySQLDb
{
    /// <inheritdoc />
    public partial class ModifiedBorrowingLogic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Editions_Borrowings_BorrowingId",
                table: "Editions");

            migrationBuilder.DropIndex(
                name: "IX_Editions_BorrowingId",
                table: "Editions");

            migrationBuilder.DropColumn(
                name: "BorrowingId",
                table: "Editions");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Borrowings",
                newName: "IsReturned");

            migrationBuilder.AddColumn<Guid>(
                name: "EditionId",
                table: "Borrowings",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"))
                .Annotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_Borrowings_EditionId",
                table: "Borrowings",
                column: "EditionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Borrowings_Editions_EditionId",
                table: "Borrowings",
                column: "EditionId",
                principalTable: "Editions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Borrowings_Editions_EditionId",
                table: "Borrowings");

            migrationBuilder.DropIndex(
                name: "IX_Borrowings_EditionId",
                table: "Borrowings");

            migrationBuilder.DropColumn(
                name: "EditionId",
                table: "Borrowings");

            migrationBuilder.RenameColumn(
                name: "IsReturned",
                table: "Borrowings",
                newName: "Status");

            migrationBuilder.AddColumn<Guid>(
                name: "BorrowingId",
                table: "Editions",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"))
                .Annotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_Editions_BorrowingId",
                table: "Editions",
                column: "BorrowingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Editions_Borrowings_BorrowingId",
                table: "Editions",
                column: "BorrowingId",
                principalTable: "Borrowings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
