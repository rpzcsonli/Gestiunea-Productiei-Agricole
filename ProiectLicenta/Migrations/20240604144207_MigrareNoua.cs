using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProiectLicenta.Migrations
{
    /// <inheritdoc />
    public partial class MigrareNoua : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Daunatori_Plante_PlanteCodPlanta",
                table: "Daunatori");

            migrationBuilder.DropForeignKey(
                name: "FK_Rasad_Plante_CodPlanta",
                table: "Rasad");

            migrationBuilder.DropForeignKey(
                name: "FK_RegistruRecoltare_Plante_PlanteCodPlanta",
                table: "RegistruRecoltare");

            migrationBuilder.DropTable(
                name: "Plante");

            migrationBuilder.DropIndex(
                name: "IX_RegistruRecoltare_PlanteCodPlanta",
                table: "RegistruRecoltare");

            migrationBuilder.DropIndex(
                name: "IX_Rasad_CodPlanta",
                table: "Rasad");

            migrationBuilder.DropIndex(
                name: "IX_Daunatori_PlanteCodPlanta",
                table: "Daunatori");

            migrationBuilder.DropColumn(
                name: "PlanteCodPlanta",
                table: "RegistruRecoltare");

            migrationBuilder.DropColumn(
                name: "CodPlanta",
                table: "Rasad");

            migrationBuilder.DropColumn(
                name: "PlanteCodPlanta",
                table: "Daunatori");

            migrationBuilder.AddColumn<string>(
                name: "Planta",
                table: "Rasad",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Contact",
                columns: table => new
                {
                    CodContact = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nume = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Subiect = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Mesaj = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contact", x => x.CodContact);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contact");

            migrationBuilder.DropColumn(
                name: "Planta",
                table: "Rasad");

            migrationBuilder.AddColumn<int>(
                name: "PlanteCodPlanta",
                table: "RegistruRecoltare",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CodPlanta",
                table: "Rasad",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PlanteCodPlanta",
                table: "Daunatori",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Plante",
                columns: table => new
                {
                    CodPlanta = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nume = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plante", x => x.CodPlanta);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RegistruRecoltare_PlanteCodPlanta",
                table: "RegistruRecoltare",
                column: "PlanteCodPlanta");

            migrationBuilder.CreateIndex(
                name: "IX_Rasad_CodPlanta",
                table: "Rasad",
                column: "CodPlanta");

            migrationBuilder.CreateIndex(
                name: "IX_Daunatori_PlanteCodPlanta",
                table: "Daunatori",
                column: "PlanteCodPlanta");

            migrationBuilder.AddForeignKey(
                name: "FK_Daunatori_Plante_PlanteCodPlanta",
                table: "Daunatori",
                column: "PlanteCodPlanta",
                principalTable: "Plante",
                principalColumn: "CodPlanta");

            migrationBuilder.AddForeignKey(
                name: "FK_Rasad_Plante_CodPlanta",
                table: "Rasad",
                column: "CodPlanta",
                principalTable: "Plante",
                principalColumn: "CodPlanta",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RegistruRecoltare_Plante_PlanteCodPlanta",
                table: "RegistruRecoltare",
                column: "PlanteCodPlanta",
                principalTable: "Plante",
                principalColumn: "CodPlanta");
        }
    }
}
