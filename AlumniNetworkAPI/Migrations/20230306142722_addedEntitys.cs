using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AlumniNetworkAPI.Migrations
{
    /// <inheritdoc />
    public partial class addedEntitys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventUser");

            migrationBuilder.DropTable(
                name: "EventUser1");

            migrationBuilder.DropTable(
                name: "GroupUser");

            migrationBuilder.CreateTable(
                name: "EventUserInvitation",
                columns: table => new
                {
                    InvitedUsersId = table.Column<int>(type: "int", nullable: false),
                    UnrespondedEventsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventUserInvitation", x => new { x.InvitedUsersId, x.UnrespondedEventsId });
                    table.ForeignKey(
                        name: "FK_EventUserInvitation_Events_UnrespondedEventsId",
                        column: x => x.UnrespondedEventsId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventUserInvitation_Users_InvitedUsersId",
                        column: x => x.InvitedUsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GroupMember",
                columns: table => new
                {
                    GroupsId = table.Column<int>(type: "int", nullable: false),
                    UsersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupMember", x => new { x.GroupsId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_GroupMember_Groups_GroupsId",
                        column: x => x.GroupsId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupMember_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RSVP",
                columns: table => new
                {
                    AcceptedEventsId = table.Column<int>(type: "int", nullable: false),
                    AcceptedUsersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RSVP", x => new { x.AcceptedEventsId, x.AcceptedUsersId });
                    table.ForeignKey(
                        name: "FK_RSVP_Events_AcceptedEventsId",
                        column: x => x.AcceptedEventsId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RSVP_Users_AcceptedUsersId",
                        column: x => x.AcceptedUsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventUserInvitation_UnrespondedEventsId",
                table: "EventUserInvitation",
                column: "UnrespondedEventsId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupMember_UsersId",
                table: "GroupMember",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_RSVP_AcceptedUsersId",
                table: "RSVP",
                column: "AcceptedUsersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventUserInvitation");

            migrationBuilder.DropTable(
                name: "GroupMember");

            migrationBuilder.DropTable(
                name: "RSVP");

            migrationBuilder.CreateTable(
                name: "EventUser",
                columns: table => new
                {
                    InvitedUsersId = table.Column<int>(type: "int", nullable: false),
                    UnrespondedEventsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventUser", x => new { x.InvitedUsersId, x.UnrespondedEventsId });
                    table.ForeignKey(
                        name: "FK_EventUser_Events_UnrespondedEventsId",
                        column: x => x.UnrespondedEventsId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventUser_Users_InvitedUsersId",
                        column: x => x.InvitedUsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EventUser1",
                columns: table => new
                {
                    AcceptedEventsId = table.Column<int>(type: "int", nullable: false),
                    AcceptedUsersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventUser1", x => new { x.AcceptedEventsId, x.AcceptedUsersId });
                    table.ForeignKey(
                        name: "FK_EventUser1_Events_AcceptedEventsId",
                        column: x => x.AcceptedEventsId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventUser1_Users_AcceptedUsersId",
                        column: x => x.AcceptedUsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GroupUser",
                columns: table => new
                {
                    GroupsId = table.Column<int>(type: "int", nullable: false),
                    UsersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupUser", x => new { x.GroupsId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_GroupUser_Groups_GroupsId",
                        column: x => x.GroupsId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupUser_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventUser_UnrespondedEventsId",
                table: "EventUser",
                column: "UnrespondedEventsId");

            migrationBuilder.CreateIndex(
                name: "IX_EventUser1_AcceptedUsersId",
                table: "EventUser1",
                column: "AcceptedUsersId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupUser_UsersId",
                table: "GroupUser",
                column: "UsersId");
        }
    }
}
