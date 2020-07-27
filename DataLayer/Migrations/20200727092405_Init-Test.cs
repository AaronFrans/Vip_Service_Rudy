using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class InitTest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    Town = table.Column<string>(nullable: false),
                    Street = table.Column<string>(nullable: false),
                    StreetNumber = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => new { x.Town, x.Street, x.StreetNumber });
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    ClientNumber = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.ClientNumber);
                });

            migrationBuilder.CreateTable(
                name: "Details",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Details", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Name = table.Column<string>(nullable: false),
                    FirstHourPrice = table.Column<int>(nullable: false),
                    Available = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "ClientDiscount",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientType = table.Column<string>(nullable: true),
                    NrOfReservationsNeeded = table.Column<int>(nullable: false),
                    Discount = table.Column<float>(nullable: false),
                    ClientNumber = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientDiscount", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientDiscount_Clients_ClientNumber",
                        column: x => x.ClientNumber,
                        principalTable: "Clients",
                        principalColumn: "ClientNumber",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReservationsPerYear",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Year = table.Column<int>(nullable: false),
                    NrOfReservations = table.Column<int>(nullable: false),
                    ClientNumber = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationsPerYear", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReservationsPerYear_Clients_ClientNumber",
                        column: x => x.ClientNumber,
                        principalTable: "Clients",
                        principalColumn: "ClientNumber",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    ReservationNumber = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReservationDate = table.Column<DateTime>(nullable: false),
                    ClientNumber = table.Column<int>(nullable: true),
                    LocationTown = table.Column<string>(nullable: true),
                    LocationStreet = table.Column<string>(nullable: true),
                    LocationStreetNumber = table.Column<string>(nullable: true),
                    DetailsId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.ReservationNumber);
                    table.ForeignKey(
                        name: "FK_Reservations_Clients_ClientNumber",
                        column: x => x.ClientNumber,
                        principalTable: "Clients",
                        principalColumn: "ClientNumber",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reservations_Details_DetailsId",
                        column: x => x.DetailsId,
                        principalTable: "Details",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reservations_Address_LocationTown_LocationStreet_LocationStreetNumber",
                        columns: x => new { x.LocationTown, x.LocationStreet, x.LocationStreetNumber },
                        principalTable: "Address",
                        principalColumns: new[] { "Town", "Street", "StreetNumber" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Arangements",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartHour = table.Column<TimeSpan>(nullable: false),
                    EndHour = table.Column<TimeSpan>(nullable: false),
                    MaxAmountOfHours = table.Column<int>(nullable: false),
                    ArangementType = table.Column<int>(nullable: false),
                    LimousineName = table.Column<string>(nullable: true),
                    Price = table.Column<int>(nullable: true),
                    ExtraHours = table.Column<int>(nullable: true),
                    Wedding_Price = table.Column<int>(nullable: true),
                    Wedding_ExtraHours = table.Column<int>(nullable: true),
                    Wellness_Price = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Arangements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Arangements_Vehicles_LimousineName",
                        column: x => x.LimousineName,
                        principalTable: "Vehicles",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HireDate",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(nullable: false),
                    LimousineName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HireDate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HireDate_Vehicles_LimousineName",
                        column: x => x.LimousineName,
                        principalTable: "Vehicles",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Arangements_LimousineName",
                table: "Arangements",
                column: "LimousineName");

            migrationBuilder.CreateIndex(
                name: "IX_ClientDiscount_ClientNumber",
                table: "ClientDiscount",
                column: "ClientNumber");

            migrationBuilder.CreateIndex(
                name: "IX_HireDate_LimousineName",
                table: "HireDate",
                column: "LimousineName");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_ClientNumber",
                table: "Reservations",
                column: "ClientNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_DetailsId",
                table: "Reservations",
                column: "DetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_LocationTown_LocationStreet_LocationStreetNumber",
                table: "Reservations",
                columns: new[] { "LocationTown", "LocationStreet", "LocationStreetNumber" });

            migrationBuilder.CreateIndex(
                name: "IX_ReservationsPerYear_ClientNumber",
                table: "ReservationsPerYear",
                column: "ClientNumber");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Arangements");

            migrationBuilder.DropTable(
                name: "ClientDiscount");

            migrationBuilder.DropTable(
                name: "HireDate");

            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "ReservationsPerYear");

            migrationBuilder.DropTable(
                name: "Vehicles");

            migrationBuilder.DropTable(
                name: "Details");

            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "Clients");
        }
    }
}
