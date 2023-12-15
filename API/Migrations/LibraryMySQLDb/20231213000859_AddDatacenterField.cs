using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BooksAPI.Migrations.LibraryMySQLDb
{
    /// <inheritdoc />
    public partial class AddDatacenterField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Adrress",
                table: "Departments",
                newName: "Address");

            migrationBuilder.AddColumn<int>(
                name: "DataCenter",
                table: "Departments",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataCenter",
                table: "Departments");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Departments",
                newName: "Adrress");
        }
    }
}
