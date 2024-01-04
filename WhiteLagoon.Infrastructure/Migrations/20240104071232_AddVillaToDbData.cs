using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WhiteLagoon.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddVillaToDbData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Villas",
                columns: new[] { "Id", "Created_Date", "Description", "ImageUrl", "Name", "Occupancy", "Price", "Sqft", "Updated_Date" },
                values: new object[,]
                {
                    { 1, null, "Villa 1 Description", "https://placehold.co/600x400", "Villa_1", 4, 200.0, 550, null },
                    { 2, null, "Villa 2 Description", "https://placehold.co/600x401", "Villa_2", 4, 300.0, 550, null },
                    { 3, null, "Villa 3 Description", "https://placehold.co/600x402", "Villa_3", 4, 400.0, 750, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
