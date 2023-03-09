using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AlumniNetworkAPI.Migrations
{
    /// <inheritdoc />
    public partial class keycloakId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "KeycloakId",
                value: "f428f142-cae0-4429-b846-991c67fc4d4f");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "KeycloakId",
                value: "");
        }
    }
}
