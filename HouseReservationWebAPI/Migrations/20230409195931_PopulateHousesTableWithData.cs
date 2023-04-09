using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HouseReservationWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class PopulateHousesTableWithData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2023, 4, 9, 21, 59, 31, 256, DateTimeKind.Local).AddTicks(106));

            migrationBuilder.InsertData(
                table: "Houses",
                columns: new[] { "Id", "Amenity", "Area", "ChargeRate", "CreatedAt", "DetailedInfo", "ImageUrl", "Name", "Occupancy", "UpdatedAt" },
                values: new object[,]
                {
                    { 2, "", 0.0, 300.0, new DateTime(2023, 4, 9, 21, 59, 31, 256, DateTimeKind.Local).AddTicks(128), "Some detailed info about the house", "", "Standard House", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "", 0.0, 400.0, new DateTime(2023, 4, 9, 21, 59, 31, 256, DateTimeKind.Local).AddTicks(130), "Some detailed info about the house", "", "Forest House", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.UpdateData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2023, 4, 9, 21, 52, 35, 705, DateTimeKind.Local).AddTicks(9897));
        }
    }
}
