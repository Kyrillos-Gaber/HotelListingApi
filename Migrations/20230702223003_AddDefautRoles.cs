using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HotelListingApi.Migrations
{
    /// <inheritdoc />
    public partial class AddDefautRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "372bc7d2-ac95-4a77-89f9-d6399b6f7ef4", "f9c48618-b58d-4dd4-b001-8afa10980bd9", "Admin", "ADMIN" },
                    { "ea37bd41-01b6-4dfe-b3b4-7e2ec3550182", "e1c2cb94-1003-4446-b9b1-e1fb96daec89", "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "372bc7d2-ac95-4a77-89f9-d6399b6f7ef4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ea37bd41-01b6-4dfe-b3b4-7e2ec3550182");
        }
    }
}
