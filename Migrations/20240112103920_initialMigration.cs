using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookLibrarySoln.Migrations
{
    /// <inheritdoc />
    public partial class initialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "LibraryAddDate", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 1, 12, 11, 39, 20, 388, DateTimeKind.Local).AddTicks(2133), new DateTime(2024, 1, 12, 11, 39, 20, 388, DateTimeKind.Local).AddTicks(2120), new DateTime(2024, 1, 12, 11, 39, 20, 388, DateTimeKind.Local).AddTicks(2281) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "LibraryAddDate", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 1, 12, 11, 35, 56, 99, DateTimeKind.Local).AddTicks(9411), new DateTime(2024, 1, 12, 11, 35, 56, 99, DateTimeKind.Local).AddTicks(9399), new DateTime(2024, 1, 12, 11, 35, 56, 99, DateTimeKind.Local).AddTicks(9414) });
        }
    }
}
