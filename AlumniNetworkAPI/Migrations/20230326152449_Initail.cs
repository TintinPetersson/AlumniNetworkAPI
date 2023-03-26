using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AlumniNetworkAPI.Migrations
{
    /// <inheritdoc />
    public partial class Initail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AuthorId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Events_Users_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GroupMember",
                columns: table => new
                {
                    GroupId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupMember", x => new { x.GroupId, x.UserId });
                    table.ForeignKey(
                        name: "FK_GroupMember_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupMember_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TopicUser",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    TopicId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TopicUser", x => new { x.UserId, x.TopicId });
                    table.ForeignKey(
                        name: "FK_TopicUser_Topics_TopicId",
                        column: x => x.TopicId,
                        principalTable: "Topics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TopicUser_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EventGroup",
                columns: table => new
                {
                    GroupsId = table.Column<int>(type: "int", nullable: false),
                    EventsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventGroup", x => new { x.GroupsId, x.EventsId });
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
                    TopicsId = table.Column<int>(type: "int", nullable: false),
                    EventsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventTopic", x => new { x.TopicsId, x.EventsId });
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
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AuthorId = table.Column<int>(type: "int", nullable: false),
                    RecieverId = table.Column<int>(type: "int", nullable: true),
                    TopicId = table.Column<int>(type: "int", nullable: true),
                    GroupId = table.Column<int>(type: "int", nullable: true),
                    EventId = table.Column<int>(type: "int", nullable: true),
                    ParentId = table.Column<int>(type: "int", nullable: true)
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
                        name: "FK_Posts_Posts_ParentId",
                        column: x => x.ParentId,
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
                columns: new[] { "Id", "AllowGuests", "AuthorId", "BannerImage", "Description", "EndTime", "LastUpdated", "Name", "StartTime" },
                values: new object[,]
                {
                    { 1, true, null, "machinelearning.jpg", "Join us for an online workshop on the basics of machine learning. Learn about various algorithms and techniques used in machine learning.", new DateTime(2023, 3, 5, 18, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Introduction to Machine Learning", new DateTime(2023, 3, 5, 10, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, true, null, "networking.jpg", "Join us for an evening of networking and socializing with fellow alumni. Make new connections, catch up with old friends, and expand your professional network.", new DateTime(2023, 3, 10, 18, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Alumni Networking Mixer", new DateTime(2023, 3, 10, 10, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, true, null, "communityservice.jpg", "Join us for a community service project to give back to the community. Help us clean up a local park and make it a better place for everyone.", new DateTime(2023, 3, 15, 18, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Community Service Project", new DateTime(2023, 3, 15, 10, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, true, null, "homecoming.jpg", "Join us for the annual alumni homecoming event. Reconnect with old friends, meet new ones, and celebrate the university's legacy.", new DateTime(2023, 3, 20, 18, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Alumni Homecoming 2023", new DateTime(2023, 3, 20, 10, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, false, null, "careerfair.jpg", "Looking for new job opportunities? Join us for the annual career fair, where you can network with employers and learn about job openings.", new DateTime(2023, 3, 25, 18, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Career Fair 2023", new DateTime(2023, 3, 25, 10, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "Id", "Description", "IsPrivate", "Name" },
                values: new object[,]
                {
                    { 1, "A group for alumni who are interested in sports and fitness.", true, "Alumni Sports Club" },
                    { 2, "A group for alumni who are interested in business networking and entrepreneurship.", false, "Business Networking Group" },
                    { 3, "A group for alumni who studied engineering at the university.", false, "Engineering Alumni Group" },
                    { 4, "An association for all alumni of the law school.", false, "Law School Alumni Association" },
                    { 5, "An association for all alumni of the medical school.", false, "Medical Alumni Association" },
                    { 6, "A group for alumni who love to read and discuss books.", true, "Alumni Book Club" },
                    { 7, "A group for alumni who love to travel and explore new places.", true, "Alumni Travel Group" }
                });

            migrationBuilder.InsertData(
                table: "Topics",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Topics related to engineering, including mechanical, electrical, civil, and chemical engineering.", "Engineering" },
                    { 2, "Topics related to law, including civil law, criminal law, international law, and human rights.", "Law" },
                    { 3, "Topics related to business, including accounting, finance, marketing, and management.", "Business" },
                    { 4, "Topics related to medicine, including anatomy, physiology, pharmacology, and pathology.", "Medicine" },
                    { 5, "Topics related to education, including teaching methods, curriculum design, educational psychology, and special education.", "Education" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Bio", "FunFact", "KeycloakId", "Picture", "Status", "Username" },
                values: new object[,]
                {
                    { 1, "Software engineer by profession. Love to play football in free time.", "I can solve a Rubik's cube in under a minute.", "7a2a4108-21b7-4a7b-9e15-415262ef547d", "https://cdn-icons-png.flaticon.com/512/3135/3135715.png", "Working", "John" },
                    { 2, "Marketing professional who loves to read and travel.", "I have visited 10 countries in the last year.", "f428f142-cae0-4429-b846-991c67fc4d4f", "https://cdn-icons-png.flaticon.com/512/3135/3135715.png", "Working", "Jane" },
                    { 3, "Computer science major at Chalmers University. Enjoys playing video games and watching sci-fi movies.", "I have won several programming competitions.", "5da8c3d6-3edc-4ff4-9f80-d75a35ca470c", "https://cdn-icons-png.flaticon.com/512/3135/3135715.png", "Student", "Alice" },
                    { 4, "Studying psychology at Yale University. Passionate about music and plays the guitar.", "I can play the guitar upside down and backwards.", "82070311-b1ca-4828-bb68-9a62b957361e", "https://cdn-icons-png.flaticon.com/512/3135/3135715.png", "Student", "Bob" },
                    { 5, "Accountant by profession. Loves to hike and explore nature.", "I have climbed Mount Kilimanjaro.", "4a7663b6-6940-46c3-86ef-659f014ae8d1", "https://cdn-icons-png.flaticon.com/512/3135/3135715.png", "Working", "Sarah" },
                    { 6, "Sales executive who enjoys playing basketball and cooking.", "I have cooked for a Michelin-starred chef.", "b6781ded-8de2-416f-b03b-0fade74353fe", "https://cdn-icons-png.flaticon.com/512/3135/3135715.png", "Working", "David" },
                    { 7, "Studying medicine at LMN University. Enjoys practicing yoga and volunteering at a local hospital.", "I can hold a handstand for over a minute.", "1cf5b92c-a998-41e0-9ad2-d7c2ff1c2673", "https://cdn-icons-png.flaticon.com/512/3135/3135715.png", "Student", "Emily" },
                    { 8, "Studying finance at PQR College. Enjoys playing tennis and watching documentaries.", "I have won several tennis tournaments in my city.", "cd42f358-b591-4f7b-a6d3-4bd8e0b840af", "https://cdn-icons-png.flaticon.com/512/3135/3135715.png", "Student", "Chris" },
                    { 9, "Graphic designer by profession. Enjoys painting and hiking on weekends.", "I have exhibited my paintings in several art galleries.", "f0b6c858-24bc-4d03-b89e-6269f8c43241", "https://cdn-icons-png.flaticon.com/512/3135/3135715.png", "Working", "Avery" }
                });

            migrationBuilder.InsertData(
                table: "EventGroup",
                columns: new[] { "EventsId", "GroupsId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 },
                    { 2, 3 },
                    { 3, 3 },
                    { 4, 5 },
                    { 5, 6 }
                });

            migrationBuilder.InsertData(
                table: "EventTopic",
                columns: new[] { "EventsId", "TopicsId" },
                values: new object[,]
                {
                    { 1, 2 },
                    { 5, 2 },
                    { 3, 3 },
                    { 4, 4 },
                    { 5, 4 },
                    { 2, 5 }
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
                    { 2, 4 },
                    { 2, 8 },
                    { 3, 5 },
                    { 3, 6 },
                    { 4, 2 },
                    { 5, 2 },
                    { 5, 4 },
                    { 6, 3 },
                    { 6, 6 },
                    { 7, 8 },
                    { 7, 9 }
                });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "AuthorId", "Body", "EventId", "GroupId", "LastUpdated", "ParentId", "RecieverId", "Title", "TopicId" },
                values: new object[,]
                {
                    { 1, 1, "As an electrical engineering major, I have learned a lot during my time in college. Here are some tips for success: 1. Stay organized and manage your time well. 2. Take advantage of networking opportunities. 3. Get hands-on experience through internships or research. 4. Stay up-to-date with the latest technologies and trends. Good luck!", 1, 1, new DateTime(2023, 3, 26, 17, 24, 48, 888, DateTimeKind.Local).AddTicks(1868), null, null, "Tips for Electrical Engineering Students", 1 },
                    { 2, 2, "If you're planning to take the bar exam, it's important to start preparing early. Here are some tips: 1. Create a study schedule and stick to it. 2. Take practice exams to get familiar with the format and types of questions. 3. Join a study group to stay motivated and get support. 4. Take care of yourself and manage your stress. You can do it!", 2, 2, new DateTime(2023, 3, 26, 17, 24, 48, 888, DateTimeKind.Local).AddTicks(2183), null, null, "Preparing for the Bar Exam", 2 },
                    { 3, 3, "Starting a small business can be challenging, but also very rewarding. Here are some tips to help you get started: 1. Conduct market research to identify a need for your product or service. 2. Create a business plan and set clear goals. 3. Secure funding and resources. 4. Build a strong team and culture. Good luck on your entrepreneurial journey!", 3, 3, new DateTime(2023, 3, 26, 17, 24, 48, 888, DateTimeKind.Local).AddTicks(2192), null, null, "Tips for Starting a Small Business", 3 },
                    { 4, 4, "As a medical resident, I have had many challenges and opportunities for growth. Here are some things I have learned: 1. Time management is key. 2. Communication skills are critical. 3. Self-care is important to avoid burnout. 4. Learning is a lifelong process. Best of luck to all medical residents!", 1, 4, new DateTime(2023, 3, 26, 17, 24, 48, 888, DateTimeKind.Local).AddTicks(2196), null, null, "My Experience as a Medical Resident", 4 },
                    { 5, 5, "STEM education is crucial for the future of our society and economy. Here are some reasons why: 1. STEM careers are in high demand and offer high salaries. 2. STEM skills are needed for innovation and problem-solving. 3. STEM education can promote diversity and social equality. Let's encourage more young people to pursue STEM education!", 4, 1, new DateTime(2023, 3, 26, 17, 24, 48, 888, DateTimeKind.Local).AddTicks(2200), null, null, "The Importance of STEM Education", 1 }
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
                table: "TopicUser",
                columns: new[] { "TopicId", "UserId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 },
                    { 1, 4 },
                    { 2, 4 },
                    { 1, 5 },
                    { 3, 5 },
                    { 4, 7 },
                    { 5, 8 },
                    { 3, 9 }
                });

            migrationBuilder.InsertData(
                table: "UserEvents",
                columns: new[] { "EventId", "UserId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 3, 1 },
                    { 5, 1 },
                    { 2, 2 },
                    { 3, 2 },
                    { 2, 3 },
                    { 4, 3 },
                    { 1, 4 },
                    { 2, 4 },
                    { 1, 5 },
                    { 3, 5 },
                    { 4, 6 },
                    { 4, 7 },
                    { 5, 8 },
                    { 5, 9 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventGroup_EventsId",
                table: "EventGroup",
                column: "EventsId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_AuthorId",
                table: "Events",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_EventTopic_EventsId",
                table: "EventTopic",
                column: "EventsId");

            migrationBuilder.CreateIndex(
                name: "IX_EventUserInvitation_UnrespondedEventsId",
                table: "EventUserInvitation",
                column: "UnrespondedEventsId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupMember_UserId",
                table: "GroupMember",
                column: "UserId");

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
                name: "IX_Posts_ParentId",
                table: "Posts",
                column: "ParentId");

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
                name: "IX_TopicUser_TopicId",
                table: "TopicUser",
                column: "TopicId");

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
