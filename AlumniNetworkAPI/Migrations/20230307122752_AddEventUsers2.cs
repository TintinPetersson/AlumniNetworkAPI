using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AlumniNetworkAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddEventUsers2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RSVP_Events_AcceptedEventsId",
                table: "RSVP");

            migrationBuilder.DropForeignKey(
                name: "FK_RSVP_Users_AcceptedUsersId",
                table: "RSVP");

            migrationBuilder.RenameColumn(
                name: "AcceptedUsersId",
                table: "RSVP",
                newName: "EventId");

            migrationBuilder.RenameColumn(
                name: "AcceptedEventsId",
                table: "RSVP",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_RSVP_AcceptedUsersId",
                table: "RSVP",
                newName: "IX_RSVP_EventId");

            migrationBuilder.AlterColumn<string>(
                name: "KeycloakId",
                table: "Users",
                type: "nvarchar(max)",
                maxLength: 2147483647,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "RSVP",
                columns: new[] { "EventId", "UserId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 3, 1 },
                    { 2, 2 },
                    { 3, 2 },
                    { 2, 3 },
                    { 1, 4 },
                    { 2, 4 },
                    { 1, 5 },
                    { 3, 5 }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "KeycloakId",
                value: "2ff40a61-2dea-418b-ad08-06aa0e0498fb");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "KeycloakId",
                value: "");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "KeycloakId",
                value: "");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                column: "KeycloakId",
                value: "");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5,
                column: "KeycloakId",
                value: "");

            migrationBuilder.AddForeignKey(
                name: "FK_RSVP_Events_EventId",
                table: "RSVP",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RSVP_Users_UserId",
                table: "RSVP",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RSVP_Events_EventId",
                table: "RSVP");

            migrationBuilder.DropForeignKey(
                name: "FK_RSVP_Users_UserId",
                table: "RSVP");

            migrationBuilder.DeleteData(
                table: "RSVP",
                keyColumns: new[] { "EventId", "UserId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "RSVP",
                keyColumns: new[] { "EventId", "UserId" },
                keyValues: new object[] { 3, 1 });

            migrationBuilder.DeleteData(
                table: "RSVP",
                keyColumns: new[] { "EventId", "UserId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "RSVP",
                keyColumns: new[] { "EventId", "UserId" },
                keyValues: new object[] { 3, 2 });

            migrationBuilder.DeleteData(
                table: "RSVP",
                keyColumns: new[] { "EventId", "UserId" },
                keyValues: new object[] { 2, 3 });

            migrationBuilder.DeleteData(
                table: "RSVP",
                keyColumns: new[] { "EventId", "UserId" },
                keyValues: new object[] { 1, 4 });

            migrationBuilder.DeleteData(
                table: "RSVP",
                keyColumns: new[] { "EventId", "UserId" },
                keyValues: new object[] { 2, 4 });

            migrationBuilder.DeleteData(
                table: "RSVP",
                keyColumns: new[] { "EventId", "UserId" },
                keyValues: new object[] { 1, 5 });

            migrationBuilder.DeleteData(
                table: "RSVP",
                keyColumns: new[] { "EventId", "UserId" },
                keyValues: new object[] { 3, 5 });

            migrationBuilder.RenameColumn(
                name: "EventId",
                table: "RSVP",
                newName: "AcceptedUsersId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "RSVP",
                newName: "AcceptedEventsId");

            migrationBuilder.RenameIndex(
                name: "IX_RSVP_EventId",
                table: "RSVP",
                newName: "IX_RSVP_AcceptedUsersId");

            migrationBuilder.AlterColumn<int>(
                name: "KeycloakId",
                table: "Users",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldMaxLength: 2147483647);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "KeycloakId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "KeycloakId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "KeycloakId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                column: "KeycloakId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5,
                column: "KeycloakId",
                value: 5);

            migrationBuilder.AddForeignKey(
                name: "FK_RSVP_Events_AcceptedEventsId",
                table: "RSVP",
                column: "AcceptedEventsId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RSVP_Users_AcceptedUsersId",
                table: "RSVP",
                column: "AcceptedUsersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
