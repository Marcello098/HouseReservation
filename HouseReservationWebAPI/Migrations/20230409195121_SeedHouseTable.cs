using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HouseReservationWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class SeedHouseTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Houses",
                columns: new[] { "Id", "Amenity", "Area", "ChargeRate", "CreatedAt", "DetailedInfo", "ImageUrl", "Name", "Occupancy", "UpdatedAt" },
                values: new object[] { 1, "", 0.0, 500.0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Some detailed info about the house", "", "Luxury House", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
