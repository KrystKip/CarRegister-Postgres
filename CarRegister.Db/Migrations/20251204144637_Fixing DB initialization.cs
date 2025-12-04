using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRegister.Db.Migrations
{
    /// <inheritdoc />
    public partial class FixingDBinitialization : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_cars",
                table: "cars");

            migrationBuilder.RenameTable(
                name: "cars",
                newName: "Cars");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cars",
                table: "Cars",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Cars",
                table: "Cars");

            migrationBuilder.RenameTable(
                name: "Cars",
                newName: "cars");

            migrationBuilder.AddPrimaryKey(
                name: "PK_cars",
                table: "cars",
                column: "Id");
        }
    }
}
