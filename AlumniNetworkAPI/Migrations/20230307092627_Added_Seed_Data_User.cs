using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AlumniNetworkAPI.Migrations
{
    /// <inheritdoc />
    public partial class Added_Seed_Data_User : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Bio", "FunFact", "KeycloakId", "Picture", "Status", "Username" },
                values: new object[] { 1, "Is from GBG", "Formula", 1, "Bild.png", "", "Filip" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
