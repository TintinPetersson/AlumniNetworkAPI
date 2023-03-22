using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AlumniNetworkAPI.Migrations
{
    /// <inheritdoc />
    public partial class initial2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EventId", "LastUpdated" },
                values: new object[] { 2, new DateTime(2023, 3, 22, 21, 20, 50, 61, DateTimeKind.Local).AddTicks(2859) });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 2,
                column: "LastUpdated",
                value: new DateTime(2023, 3, 22, 21, 20, 50, 61, DateTimeKind.Local).AddTicks(2900));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 3,
                column: "LastUpdated",
                value: new DateTime(2023, 3, 22, 21, 20, 50, 61, DateTimeKind.Local).AddTicks(2903));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 4,
                column: "LastUpdated",
                value: new DateTime(2023, 3, 22, 21, 20, 50, 61, DateTimeKind.Local).AddTicks(2906));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EventId", "LastUpdated" },
                values: new object[] { null, new DateTime(2023, 3, 22, 21, 19, 16, 259, DateTimeKind.Local).AddTicks(8824) });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 2,
                column: "LastUpdated",
                value: new DateTime(2023, 3, 22, 21, 19, 16, 259, DateTimeKind.Local).AddTicks(8868));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 3,
                column: "LastUpdated",
                value: new DateTime(2023, 3, 22, 21, 19, 16, 259, DateTimeKind.Local).AddTicks(8871));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 4,
                column: "LastUpdated",
                value: new DateTime(2023, 3, 22, 21, 19, 16, 259, DateTimeKind.Local).AddTicks(8873));
        }
    }
}
