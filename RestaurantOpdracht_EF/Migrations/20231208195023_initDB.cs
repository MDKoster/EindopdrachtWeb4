using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantOpdracht_EF.Migrations
{
    /// <inheritdoc />
    public partial class initDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Klant",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naam = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Tel = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    Postcode = table.Column<int>(type: "int", maxLength: 4, nullable: false),
                    Gemeentenaam = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Straatnaam = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    HuisNr = table.Column<string>(type: "nvarchar(25)", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Klant", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Restaurant",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naam = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    Keuken = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Tel = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    Postcode = table.Column<int>(type: "int", maxLength: 4, nullable: false),
                    Gemeentenaam = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Straatnaam = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    HuisNr = table.Column<string>(type: "nvarchar(25)", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Restaurant", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Reservatie",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KlantID = table.Column<int>(type: "int", nullable: false),
                    RestaurantID = table.Column<int>(type: "int", nullable: false),
                    AantalPlaatsen = table.Column<int>(type: "int", nullable: false),
                    Datum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TafelNr = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservatie", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Reservatie_Klant_KlantID",
                        column: x => x.KlantID,
                        principalTable: "Klant",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Reservatie_Restaurant_RestaurantID",
                        column: x => x.RestaurantID,
                        principalTable: "Restaurant",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Tafel",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TafelNr = table.Column<int>(type: "int", nullable: false),
                    AantalPlaatsen = table.Column<int>(type: "int", nullable: false),
                    RestaurantID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tafel", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Tafel_Restaurant_RestaurantID",
                        column: x => x.RestaurantID,
                        principalTable: "Restaurant",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reservatie_KlantID",
                table: "Reservatie",
                column: "KlantID");

            migrationBuilder.CreateIndex(
                name: "IX_Reservatie_RestaurantID",
                table: "Reservatie",
                column: "RestaurantID");

            migrationBuilder.CreateIndex(
                name: "IX_Tafel_RestaurantID",
                table: "Tafel",
                column: "RestaurantID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reservatie");

            migrationBuilder.DropTable(
                name: "Tafel");

            migrationBuilder.DropTable(
                name: "Klant");

            migrationBuilder.DropTable(
                name: "Restaurant");
        }
    }
}
