using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BooksAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddedRelationsToBorriwngsEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Editions_EditionId",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Borrowings_Users_BorrowerId",
                table: "Borrowings");

            migrationBuilder.DropForeignKey(
                name: "FK_Editions_Departments_DepartmentsId",
                table: "Editions");

            migrationBuilder.DropIndex(
                name: "IX_Books_EditionId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "EditionId",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "DepartmentsId",
                table: "Editions",
                newName: "DepartmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Editions_DepartmentsId",
                table: "Editions",
                newName: "IX_Editions_DepartmentId");

            migrationBuilder.RenameColumn(
                name: "BorrowerId",
                table: "Borrowings",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Borrowings_BorrowerId",
                table: "Borrowings",
                newName: "IX_Borrowings_UserId");

            migrationBuilder.AddColumn<Guid>(
                name: "DepartmentId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "BookId",
                table: "Editions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "BorrowingId",
                table: "Editions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Users_DepartmentId",
                table: "Users",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Editions_BookId",
                table: "Editions",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_Editions_BorrowingId",
                table: "Editions",
                column: "BorrowingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Borrowings_Users_UserId",
                table: "Borrowings",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Editions_Books_BookId",
                table: "Editions",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Editions_Borrowings_BorrowingId",
                table: "Editions",
                column: "BorrowingId",
                principalTable: "Borrowings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Editions_Departments_DepartmentId",
                table: "Editions",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Departments_DepartmentId",
                table: "Users",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Borrowings_Users_UserId",
                table: "Borrowings");

            migrationBuilder.DropForeignKey(
                name: "FK_Editions_Books_BookId",
                table: "Editions");

            migrationBuilder.DropForeignKey(
                name: "FK_Editions_Borrowings_BorrowingId",
                table: "Editions");

            migrationBuilder.DropForeignKey(
                name: "FK_Editions_Departments_DepartmentId",
                table: "Editions");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Departments_DepartmentId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_DepartmentId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Editions_BookId",
                table: "Editions");

            migrationBuilder.DropIndex(
                name: "IX_Editions_BorrowingId",
                table: "Editions");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "BookId",
                table: "Editions");

            migrationBuilder.DropColumn(
                name: "BorrowingId",
                table: "Editions");

            migrationBuilder.RenameColumn(
                name: "DepartmentId",
                table: "Editions",
                newName: "DepartmentsId");

            migrationBuilder.RenameIndex(
                name: "IX_Editions_DepartmentId",
                table: "Editions",
                newName: "IX_Editions_DepartmentsId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Borrowings",
                newName: "BorrowerId");

            migrationBuilder.RenameIndex(
                name: "IX_Borrowings_UserId",
                table: "Borrowings",
                newName: "IX_Borrowings_BorrowerId");

            migrationBuilder.AddColumn<Guid>(
                name: "EditionId",
                table: "Books",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Books_EditionId",
                table: "Books",
                column: "EditionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Editions_EditionId",
                table: "Books",
                column: "EditionId",
                principalTable: "Editions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Borrowings_Users_BorrowerId",
                table: "Borrowings",
                column: "BorrowerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Editions_Departments_DepartmentsId",
                table: "Editions",
                column: "DepartmentsId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
