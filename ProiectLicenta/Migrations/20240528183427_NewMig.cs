using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProiectLicenta.Migrations
{
    /// <inheritdoc />
    public partial class NewMig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Descriere",
                table: "Plante");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Descriere",
                table: "Plante",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}
