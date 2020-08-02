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
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Town = table.Column<string>(nullable: true),
                    Street = table.Column<string>(nullable: true),
                    StreetNumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
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
                name: "Clients",
                columns: table => new
                {
                    ClientNumber = table.Column<int>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    AddressId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    BtwNumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.ClientNumber);
                    table.ForeignKey(
                        name: "FK_Clients_Address_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Arangements",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartHourTicks = table.Column<long>(nullable: false),
                    EndHourTicks = table.Column<long>(nullable: false),
                    ArangementType = table.Column<int>(nullable: false),
                    LimousineName = table.Column<string>(nullable: true),
                    Price = table.Column<int>(nullable: true),
                    Wedding_Price = table.Column<int>(nullable: true),
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
                name: "Details",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartLocationId = table.Column<int>(nullable: true),
                    EndLocationId = table.Column<int>(nullable: true),
                    LimousineName = table.Column<string>(nullable: true),
                    DateLimousineNeeded = table.Column<DateTime>(nullable: false),
                    Arangement = table.Column<string>(nullable: true),
                    SubTotal = table.Column<int>(nullable: false),
                    UsedDiscount = table.Column<float>(nullable: false),
                    AmountWithoutBtw = table.Column<int>(nullable: false),
                    BtwPercentage = table.Column<float>(nullable: false),
                    BtwAmount = table.Column<int>(nullable: false),
                    ToPayAmount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Details", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Details_Address_EndLocationId",
                        column: x => x.EndLocationId,
                        principalTable: "Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Details_Vehicles_LimousineName",
                        column: x => x.LimousineName,
                        principalTable: "Vehicles",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Details_Address_StartLocationId",
                        column: x => x.StartLocationId,
                        principalTable: "Address",
                        principalColumn: "Id",
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

            migrationBuilder.CreateTable(
                name: "ClientDiscount",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientType = table.Column<int>(nullable: false),
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
                name: "HourType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(nullable: true),
                    NrOfHours = table.Column<int>(nullable: false),
                    TotalPrice = table.Column<int>(nullable: false),
                    DetailsId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HourType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HourType_Details_DetailsId",
                        column: x => x.DetailsId,
                        principalTable: "Details",
                        principalColumn: "Id",
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
                    LocationId = table.Column<int>(nullable: true),
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
                        name: "FK_Reservations_Address_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Address",
                        principalColumn: "Id",
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
                name: "IX_Clients_AddressId",
                table: "Clients",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Details_EndLocationId",
                table: "Details",
                column: "EndLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Details_LimousineName",
                table: "Details",
                column: "LimousineName");

            migrationBuilder.CreateIndex(
                name: "IX_Details_StartLocationId",
                table: "Details",
                column: "StartLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_HireDate_LimousineName",
                table: "HireDate",
                column: "LimousineName");

            migrationBuilder.CreateIndex(
                name: "IX_HourType_DetailsId",
                table: "HourType",
                column: "DetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_ClientNumber",
                table: "Reservations",
                column: "ClientNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_DetailsId",
                table: "Reservations",
                column: "DetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_LocationId",
                table: "Reservations",
                column: "LocationId");

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
                name: "HourType");

            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "ReservationsPerYear");

            migrationBuilder.DropTable(
                name: "Details");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Vehicles");

            migrationBuilder.DropTable(
                name: "Address");
        }
    }
}
