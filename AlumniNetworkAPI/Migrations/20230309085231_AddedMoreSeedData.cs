using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AlumniNetworkAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddedMoreSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupMember_Groups_GroupsId",
                table: "GroupMember");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupMember_Users_UsersId",
                table: "GroupMember");

            migrationBuilder.RenameColumn(
                name: "UsersId",
                table: "GroupMember",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "GroupsId",
                table: "GroupMember",
                newName: "GroupId");

            migrationBuilder.RenameIndex(
                name: "IX_GroupMember_UsersId",
                table: "GroupMember",
                newName: "IX_GroupMember_UserId");

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "Id", "Description", "IsPrivate", "Name" },
                values: new object[,]
                {
                    { 1, "ASP.NET", true, ".Net" },
                    { 2, "Java and Javascript", true, "Java" },
                    { 3, "Mikael and Erik", true, "Skövde-group" }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Bio", "KeycloakId", "Picture", "Username" },
                values: new object[] { "ya", "", "Bild2.png", "Tommy" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Bio", "Username" },
                values: new object[] { "Jag älskar skövde", "Mikael" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Bio", "FunFact", "KeycloakId", "Picture", "Status", "Username" },
                values: new object[,]
                {
                    { 6, "Jag älskar också skövde", "Gillar att koda", "", "Bild6.png", "", "Erik" },
                    { 7, "Jag får andra att skratta", "React är min grej", "", "Bild7.png", "", "Alexander" },
                    { 8, "Jag gillar promenader", "Bra på Frontend", "", "Bild8.png", "", "Amanda" },
                    { 9, "Jag gillar promenader", "Bra på Frontend", "qweqwe", "Bild9.png", "", "Manda" }
                });

            migrationBuilder.InsertData(
                table: "GroupMember",
                columns: new[] { "GroupId", "UserId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 1, 3 },
                    { 1, 4 },
                    { 1, 5 },
                    { 1, 6 },
                    { 2, 7 },
                    { 2, 8 },
                    { 3, 5 },
                    { 3, 6 }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_GroupMember_Groups_GroupId",
                table: "GroupMember",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupMember_Users_UserId",
                table: "GroupMember",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupMember_Groups_GroupId",
                table: "GroupMember");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupMember_Users_UserId",
                table: "GroupMember");

            migrationBuilder.DeleteData(
                table: "GroupMember",
                keyColumns: new[] { "GroupId", "UserId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "GroupMember",
                keyColumns: new[] { "GroupId", "UserId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "GroupMember",
                keyColumns: new[] { "GroupId", "UserId" },
                keyValues: new object[] { 1, 3 });

            migrationBuilder.DeleteData(
                table: "GroupMember",
                keyColumns: new[] { "GroupId", "UserId" },
                keyValues: new object[] { 1, 4 });

            migrationBuilder.DeleteData(
                table: "GroupMember",
                keyColumns: new[] { "GroupId", "UserId" },
                keyValues: new object[] { 1, 5 });

            migrationBuilder.DeleteData(
                table: "GroupMember",
                keyColumns: new[] { "GroupId", "UserId" },
                keyValues: new object[] { 1, 6 });

            migrationBuilder.DeleteData(
                table: "GroupMember",
                keyColumns: new[] { "GroupId", "UserId" },
                keyValues: new object[] { 2, 7 });

            migrationBuilder.DeleteData(
                table: "GroupMember",
                keyColumns: new[] { "GroupId", "UserId" },
                keyValues: new object[] { 2, 8 });

            migrationBuilder.DeleteData(
                table: "GroupMember",
                keyColumns: new[] { "GroupId", "UserId" },
                keyValues: new object[] { 3, 5 });

            migrationBuilder.DeleteData(
                table: "GroupMember",
                keyColumns: new[] { "GroupId", "UserId" },
                keyValues: new object[] { 3, 6 });

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "GroupMember",
                newName: "UsersId");

            migrationBuilder.RenameColumn(
                name: "GroupId",
                table: "GroupMember",
                newName: "GroupsId");

            migrationBuilder.RenameIndex(
                name: "IX_GroupMember_UserId",
                table: "GroupMember",
                newName: "IX_GroupMember_UsersId");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Bio", "KeycloakId", "Picture", "Username" },
                values: new object[] { "", "f428f142-cae0-4429-b846-991c67fc4d4f", "", "admin" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Bio", "Username" },
                values: new object[] { "Älska, älska inte", "Annika" });

            migrationBuilder.AddForeignKey(
                name: "FK_GroupMember_Groups_GroupsId",
                table: "GroupMember",
                column: "GroupsId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupMember_Users_UsersId",
                table: "GroupMember",
                column: "UsersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
