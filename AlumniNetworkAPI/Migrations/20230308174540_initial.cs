using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AlumniNetworkAPI.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    AllowGuests = table.Column<bool>(type: "bit", nullable: false),
                    BannerImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    IsPrivate = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Topics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Topics", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KeycloakId = table.Column<string>(type: "nvarchar(max)", maxLength: 2147483647, nullable: false),
                    Username = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Picture = table.Column<string>(type: "nvarchar(max)", maxLength: 2147483647, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Bio = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    FunFact = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EventGroup",
                columns: table => new
                {
                    EventsId = table.Column<int>(type: "int", nullable: false),
                    GroupsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventGroup", x => new { x.EventsId, x.GroupsId });
                    table.ForeignKey(
                        name: "FK_EventGroup_Events_EventsId",
                        column: x => x.EventsId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventGroup_Groups_GroupsId",
                        column: x => x.GroupsId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EventTopic",
                columns: table => new
                {
                    EventsId = table.Column<int>(type: "int", nullable: false),
                    TopicsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventTopic", x => new { x.EventsId, x.TopicsId });
                    table.ForeignKey(
                        name: "FK_EventTopic_Events_EventsId",
                        column: x => x.EventsId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventTopic_Topics_TopicsId",
                        column: x => x.TopicsId,
                        principalTable: "Topics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AuthorId = table.Column<int>(type: "int", nullable: false),
                    RecieverId = table.Column<int>(type: "int", nullable: true),
                    TopicId = table.Column<int>(type: "int", nullable: true),
                    GroupId = table.Column<int>(type: "int", nullable: true),
                    EventId = table.Column<int>(type: "int", nullable: true),
                    ParentPostId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Posts_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Posts_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Posts_Posts_ParentPostId",
                        column: x => x.ParentPostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Posts_Topics_TopicId",
                        column: x => x.TopicId,
                        principalTable: "Topics",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Posts_Users_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Posts_Users_RecieverId",
                        column: x => x.RecieverId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RSVP",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    EventId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RSVP", x => new { x.UserId, x.EventId });
                    table.ForeignKey(
                        name: "FK_RSVP_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RSVP_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TopicUser",
                columns: table => new
                {
                    TopicsId = table.Column<int>(type: "int", nullable: false),
                    UsersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TopicUser", x => new { x.TopicsId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_TopicUser_Topics_TopicsId",
                        column: x => x.TopicsId,
                        principalTable: "Topics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TopicUser_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserEvents",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    EventId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserEvents", x => new { x.UserId, x.EventId });
                    table.ForeignKey(
                        name: "FK_UserEvents_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserEvents_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "AllowGuests", "BannerImage", "Description", "EndTime", "LastUpdated", "Name", "StartTime" },
                values: new object[,]
                {
                    { 1, true, "Image", "Bild.png", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Webinar", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, true, "Image", "Bild.png", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "AW", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, true, "Image", "Bild.png", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Meet-up", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Bio", "FunFact", "KeycloakId", "Picture", "Status", "Username" },
                values: new object[,]
                {
                    { 1, "Is from GBG", "Formula", "2ff40a61-2dea-418b-ad08-06aa0e0498fb", "Bild.png", "", "Filip" },
                    { 2, "", "Big Formula", "f428f142-cae0-4429-b846-991c67fc4d4f", "", "", "admin" },
                    { 3, "Karate", "Artist", "", "Bild3.png", "", "Maryam" },
                    { 4, "Varberg", ".NET utvecklare", "", "Bild4.png", "", "TinTin" },
                    { 5, "Älska, älska inte", "Bestämd", "", "Bild5.png", "", "Annika" }
                });

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

            migrationBuilder.InsertData(
                table: "UserEvents",
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

            migrationBuilder.CreateIndex(
                name: "IX_EventGroup_GroupsId",
                table: "EventGroup",
                column: "GroupsId");

            migrationBuilder.CreateIndex(
                name: "IX_EventTopic_TopicsId",
                table: "EventTopic",
                column: "TopicsId");

            migrationBuilder.CreateIndex(
                name: "IX_EventUserInvitation_UnrespondedEventsId",
                table: "EventUserInvitation",
                column: "UnrespondedEventsId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupMember_UsersId",
                table: "GroupMember",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_AuthorId",
                table: "Posts",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_EventId",
                table: "Posts",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_GroupId",
                table: "Posts",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_ParentPostId",
                table: "Posts",
                column: "ParentPostId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_RecieverId",
                table: "Posts",
                column: "RecieverId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_TopicId",
                table: "Posts",
                column: "TopicId");

            migrationBuilder.CreateIndex(
                name: "IX_RSVP_EventId",
                table: "RSVP",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_TopicUser_UsersId",
                table: "TopicUser",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_UserEvents_EventId",
                table: "UserEvents",
                column: "EventId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventGroup");

            migrationBuilder.DropTable(
                name: "EventTopic");

            migrationBuilder.DropTable(
                name: "EventUserInvitation");

            migrationBuilder.DropTable(
                name: "GroupMember");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "RSVP");

            migrationBuilder.DropTable(
                name: "TopicUser");

            migrationBuilder.DropTable(
                name: "UserEvents");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "Topics");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
