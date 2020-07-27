using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class InitTest_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndHour",
                table: "Arangements");

            migrationBuilder.DropColumn(
                name: "StartHour",
                table: "Arangements");

            migrationBuilder.AlterColumn<int>(
                name: "ClientType",
                table: "ClientDiscount",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "EndHourTicks",
                table: "Arangements",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "StartHourTicks",
                table: "Arangements",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndHourTicks",
                table: "Arangements");

            migrationBuilder.DropColumn(
                name: "StartHourTicks",
                table: "Arangements");

            migrationBuilder.AlterColumn<string>(
                name: "ClientType",
                table: "ClientDiscount",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "EndHour",
                table: "Arangements",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "StartHour",
                table: "Arangements",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }
    }
}
