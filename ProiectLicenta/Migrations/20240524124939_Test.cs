using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProiectLicenta.Migrations
{
    /// <inheritdoc />
    public partial class Test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Angajat",
                columns: table => new
                {
                    CodAngajat = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nume = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Prenume = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Functie = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Telefon = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Angajat", x => x.CodAngajat);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Plante",
                columns: table => new
                {
                    CodPlanta = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nume = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Descriere = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plante", x => x.CodPlanta);
                });

            migrationBuilder.CreateTable(
                name: "Tratament",
                columns: table => new
                {
                    CodTratament = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Denumire = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Cantitate = table.Column<int>(type: "int", nullable: false),
                    Perioada = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tratament", x => x.CodTratament);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rasad",
                columns: table => new
                {
                    CodRasad = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Denumire = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CodPlanta = table.Column<int>(type: "int", nullable: false),
                    DataSemanat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataMaturitate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Cantitate = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rasad", x => x.CodRasad);
                    table.ForeignKey(
                        name: "FK_Rasad_Plante_CodPlanta",
                        column: x => x.CodPlanta,
                        principalTable: "Plante",
                        principalColumn: "CodPlanta",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Daunatori",
                columns: table => new
                {
                    CodDaunator = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Denumire = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CodTratament = table.Column<int>(type: "int", nullable: false),
                    PlanteCodPlanta = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Daunatori", x => x.CodDaunator);
                    table.ForeignKey(
                        name: "FK_Daunatori_Plante_PlanteCodPlanta",
                        column: x => x.PlanteCodPlanta,
                        principalTable: "Plante",
                        principalColumn: "CodPlanta");
                    table.ForeignKey(
                        name: "FK_Daunatori_Tratament_CodTratament",
                        column: x => x.CodTratament,
                        principalTable: "Tratament",
                        principalColumn: "CodTratament",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Parcela",
                columns: table => new
                {
                    CodParcela = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Locatie = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Tip = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Suprafata = table.Column<int>(type: "int", nullable: false),
                    CodRasad = table.Column<int>(type: "int", nullable: false),
                    NumarPlante = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parcela", x => x.CodParcela);
                    table.ForeignKey(
                        name: "FK_Parcela_Rasad_CodRasad",
                        column: x => x.CodRasad,
                        principalTable: "Rasad",
                        principalColumn: "CodRasad",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RegistruCopilire",
                columns: table => new
                {
                    CodCopilire = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodParcela = table.Column<int>(type: "int", nullable: false),
                    NumarPlante = table.Column<int>(type: "int", nullable: false),
                    CodAngajat = table.Column<int>(type: "int", nullable: false),
                    DataCopilire = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistruCopilire", x => x.CodCopilire);
                    table.ForeignKey(
                        name: "FK_RegistruCopilire_Angajat_CodAngajat",
                        column: x => x.CodAngajat,
                        principalTable: "Angajat",
                        principalColumn: "CodAngajat",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RegistruCopilire_Parcela_CodParcela",
                        column: x => x.CodParcela,
                        principalTable: "Parcela",
                        principalColumn: "CodParcela",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RegistruFertilizare",
                columns: table => new
                {
                    CodFertilizare = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodParcela = table.Column<int>(type: "int", nullable: false),
                    Suprafata = table.Column<int>(type: "int", nullable: false),
                    CodAngajat = table.Column<int>(type: "int", nullable: false),
                    DataFertilizare = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistruFertilizare", x => x.CodFertilizare);
                    table.ForeignKey(
                        name: "FK_RegistruFertilizare_Angajat_CodAngajat",
                        column: x => x.CodAngajat,
                        principalTable: "Angajat",
                        principalColumn: "CodAngajat",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RegistruFertilizare_Parcela_CodParcela",
                        column: x => x.CodParcela,
                        principalTable: "Parcela",
                        principalColumn: "CodParcela",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RegistruIrigare",
                columns: table => new
                {
                    CodIrigare = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodParcela = table.Column<int>(type: "int", nullable: false),
                    DurataIrigare = table.Column<int>(type: "int", nullable: false),
                    CodAngajat = table.Column<int>(type: "int", nullable: false),
                    DataIrigare = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistruIrigare", x => x.CodIrigare);
                    table.ForeignKey(
                        name: "FK_RegistruIrigare_Angajat_CodAngajat",
                        column: x => x.CodAngajat,
                        principalTable: "Angajat",
                        principalColumn: "CodAngajat",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RegistruIrigare_Parcela_CodParcela",
                        column: x => x.CodParcela,
                        principalTable: "Parcela",
                        principalColumn: "CodParcela",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RegistruPalisare",
                columns: table => new
                {
                    CodPalisare = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodParcela = table.Column<int>(type: "int", nullable: false),
                    NumarPlante = table.Column<int>(type: "int", nullable: false),
                    CodAngajat = table.Column<int>(type: "int", nullable: false),
                    DataPalisare = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistruPalisare", x => x.CodPalisare);
                    table.ForeignKey(
                        name: "FK_RegistruPalisare_Angajat_CodAngajat",
                        column: x => x.CodAngajat,
                        principalTable: "Angajat",
                        principalColumn: "CodAngajat",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RegistruPalisare_Parcela_CodParcela",
                        column: x => x.CodParcela,
                        principalTable: "Parcela",
                        principalColumn: "CodParcela",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RegistruRecoltare",
                columns: table => new
                {
                    CodRecoltare = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodParcela = table.Column<int>(type: "int", nullable: false),
                    CodAngajat = table.Column<int>(type: "int", nullable: false),
                    CantitateRecoltata = table.Column<int>(type: "int", nullable: false),
                    DataRecoltare = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PlanteCodPlanta = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistruRecoltare", x => x.CodRecoltare);
                    table.ForeignKey(
                        name: "FK_RegistruRecoltare_Angajat_CodAngajat",
                        column: x => x.CodAngajat,
                        principalTable: "Angajat",
                        principalColumn: "CodAngajat",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RegistruRecoltare_Parcela_CodParcela",
                        column: x => x.CodParcela,
                        principalTable: "Parcela",
                        principalColumn: "CodParcela",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RegistruRecoltare_Plante_PlanteCodPlanta",
                        column: x => x.PlanteCodPlanta,
                        principalTable: "Plante",
                        principalColumn: "CodPlanta");
                });

            migrationBuilder.CreateTable(
                name: "RegistruTratamente",
                columns: table => new
                {
                    CodTratamentAplicat = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodParcela = table.Column<int>(type: "int", nullable: false),
                    CodDaunator = table.Column<int>(type: "int", nullable: false),
                    Suprafata = table.Column<int>(type: "int", nullable: false),
                    CodAngajat = table.Column<int>(type: "int", nullable: false),
                    DataAplicare = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistruTratamente", x => x.CodTratamentAplicat);
                    table.ForeignKey(
                        name: "FK_RegistruTratamente_Angajat_CodAngajat",
                        column: x => x.CodAngajat,
                        principalTable: "Angajat",
                        principalColumn: "CodAngajat",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RegistruTratamente_Daunatori_CodDaunator",
                        column: x => x.CodDaunator,
                        principalTable: "Daunatori",
                        principalColumn: "CodDaunator",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RegistruTratamente_Parcela_CodParcela",
                        column: x => x.CodParcela,
                        principalTable: "Parcela",
                        principalColumn: "CodParcela",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Daunatori_CodTratament",
                table: "Daunatori",
                column: "CodTratament");

            migrationBuilder.CreateIndex(
                name: "IX_Daunatori_PlanteCodPlanta",
                table: "Daunatori",
                column: "PlanteCodPlanta");

            migrationBuilder.CreateIndex(
                name: "IX_Parcela_CodRasad",
                table: "Parcela",
                column: "CodRasad");

            migrationBuilder.CreateIndex(
                name: "IX_Rasad_CodPlanta",
                table: "Rasad",
                column: "CodPlanta");

            migrationBuilder.CreateIndex(
                name: "IX_RegistruCopilire_CodAngajat",
                table: "RegistruCopilire",
                column: "CodAngajat");

            migrationBuilder.CreateIndex(
                name: "IX_RegistruCopilire_CodParcela",
                table: "RegistruCopilire",
                column: "CodParcela");

            migrationBuilder.CreateIndex(
                name: "IX_RegistruFertilizare_CodAngajat",
                table: "RegistruFertilizare",
                column: "CodAngajat");

            migrationBuilder.CreateIndex(
                name: "IX_RegistruFertilizare_CodParcela",
                table: "RegistruFertilizare",
                column: "CodParcela");

            migrationBuilder.CreateIndex(
                name: "IX_RegistruIrigare_CodAngajat",
                table: "RegistruIrigare",
                column: "CodAngajat");

            migrationBuilder.CreateIndex(
                name: "IX_RegistruIrigare_CodParcela",
                table: "RegistruIrigare",
                column: "CodParcela");

            migrationBuilder.CreateIndex(
                name: "IX_RegistruPalisare_CodAngajat",
                table: "RegistruPalisare",
                column: "CodAngajat");

            migrationBuilder.CreateIndex(
                name: "IX_RegistruPalisare_CodParcela",
                table: "RegistruPalisare",
                column: "CodParcela");

            migrationBuilder.CreateIndex(
                name: "IX_RegistruRecoltare_CodAngajat",
                table: "RegistruRecoltare",
                column: "CodAngajat");

            migrationBuilder.CreateIndex(
                name: "IX_RegistruRecoltare_CodParcela",
                table: "RegistruRecoltare",
                column: "CodParcela");

            migrationBuilder.CreateIndex(
                name: "IX_RegistruRecoltare_PlanteCodPlanta",
                table: "RegistruRecoltare",
                column: "PlanteCodPlanta");

            migrationBuilder.CreateIndex(
                name: "IX_RegistruTratamente_CodAngajat",
                table: "RegistruTratamente",
                column: "CodAngajat");

            migrationBuilder.CreateIndex(
                name: "IX_RegistruTratamente_CodDaunator",
                table: "RegistruTratamente",
                column: "CodDaunator");

            migrationBuilder.CreateIndex(
                name: "IX_RegistruTratamente_CodParcela",
                table: "RegistruTratamente",
                column: "CodParcela");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "RegistruCopilire");

            migrationBuilder.DropTable(
                name: "RegistruFertilizare");

            migrationBuilder.DropTable(
                name: "RegistruIrigare");

            migrationBuilder.DropTable(
                name: "RegistruPalisare");

            migrationBuilder.DropTable(
                name: "RegistruRecoltare");

            migrationBuilder.DropTable(
                name: "RegistruTratamente");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Angajat");

            migrationBuilder.DropTable(
                name: "Daunatori");

            migrationBuilder.DropTable(
                name: "Parcela");

            migrationBuilder.DropTable(
                name: "Tratament");

            migrationBuilder.DropTable(
                name: "Rasad");

            migrationBuilder.DropTable(
                name: "Plante");
        }
    }
}
