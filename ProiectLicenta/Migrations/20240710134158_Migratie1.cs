using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProiectLicenta.Migrations
{
    /// <inheritdoc />
    public partial class Migratie1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Stare",
                table: "RegistruTratamente",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Stare",
                table: "RegistruRecoltare",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Stare",
                table: "RegistruPalisare",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Stare",
                table: "RegistruIrigare",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Stare",
                table: "RegistruFertilizare",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Stare",
                table: "RegistruCopilire",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Stare",
                table: "RegistruTratamente");

            migrationBuilder.DropColumn(
                name: "Stare",
                table: "RegistruRecoltare");

            migrationBuilder.DropColumn(
                name: "Stare",
                table: "RegistruPalisare");

            migrationBuilder.DropColumn(
                name: "Stare",
                table: "RegistruIrigare");

            migrationBuilder.DropColumn(
                name: "Stare",
                table: "RegistruFertilizare");

            migrationBuilder.DropColumn(
                name: "Stare",
                table: "RegistruCopilire");
        }
    }
}
