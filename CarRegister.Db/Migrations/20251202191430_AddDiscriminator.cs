using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRegister.Db.Migrations
{
    /// <inheritdoc />
    public partial class AddDiscriminator : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Cars",
                table: "cars");

            migrationBuilder.RenameTable(
                name: "cars",
                newName: "cars");

            migrationBuilder.AlterColumn<int>(
                name: "BatteryCapacity",
                table: "cars",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<string>(
                name: "CarType",
                table: "cars",
                type: "character varying(8)",
                maxLength: 8,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_cars",
                table: "cars",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_cars",
                table: "cars");

            migrationBuilder.DropColumn(
                name: "CarType",
                table: "cars");

            migrationBuilder.RenameTable(
                name: "cars",
                newName: "cars");

            migrationBuilder.AlterColumn<int>(
                name: "BatteryCapacity",
                table: "cars",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cars",
                table: "cars",
                column: "Id");
        }
    }
}
