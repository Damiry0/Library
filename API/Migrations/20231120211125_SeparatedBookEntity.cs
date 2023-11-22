using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BooksAPI.Migrations
{
    /// <inheritdoc />
    public partial class SeparatedBookEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Books",
                newName: "Remaining");

            migrationBuilder.AlterColumn<int>(
                name: "Pages",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "EditionId",
                table: "Books",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Editions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DepartmentsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Editions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Editions_Departments_DepartmentsId",
                        column: x => x.DepartmentsId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_EditionId",
                table: "Books",
                column: "EditionId");

            migrationBuilder.CreateIndex(
                name: "IX_Editions_DepartmentsId",
                table: "Editions",
                column: "DepartmentsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Editions_EditionId",
                table: "Books",
                column: "EditionId",
                principalTable: "Editions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Editions_EditionId",
                table: "Books");

            migrationBuilder.DropTable(
                name: "Editions");

            migrationBuilder.DropIndex(
                name: "IX_Books_EditionId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "EditionId",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "Remaining",
                table: "Books",
                newName: "Status");

            migrationBuilder.AlterColumn<int>(
                name: "Pages",
                table: "Books",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
