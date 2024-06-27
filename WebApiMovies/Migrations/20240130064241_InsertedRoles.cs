using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiMovies.Migrations
{
    public partial class InsertedRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "85a6d25d-79c2-4cf3-8adc-b4c2ffbf1a23");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d94e445e-abce-4be4-9a50-f58cc4a745f8");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "35168931-bdd6-4e57-b7ef-72cc517041a1", "b2513084-e9bf-4290-b469-5dc734daa954", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "cec9f1f3-c42a-4346-ae54-e73d9028f2ff", "73fe9717-30d2-459e-8c1d-0d02946faf2a", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "35168931-bdd6-4e57-b7ef-72cc517041a1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cec9f1f3-c42a-4346-ae54-e73d9028f2ff");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "85a6d25d-79c2-4cf3-8adc-b4c2ffbf1a23", "07fad923-fe5b-414d-b81f-457233288d7a", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d94e445e-abce-4be4-9a50-f58cc4a745f8", "922adf89-30f5-4df1-94df-3ef1865ba250", "User", "VISITOR" });
        }
    }
}
