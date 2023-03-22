﻿// <auto-generated />
using System;
using AlumniNetworkAPI.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AlumniNetworkAPI.Migrations
{
    [DbContext(typeof(AlumniNetworkDbContext))]
    partial class AlumniNetworkDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AlumniNetworkAPI.Models.Domain.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("AllowGuests")
                        .HasColumnType("bit");

                    b.Property<int?>("AuthorId")
                        .HasColumnType("int");

                    b.Property<string>("BannerImage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("LastUpdated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.ToTable("Events");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AllowGuests = true,
                            BannerImage = "Image",
                            Description = "Bild.png",
                            EndTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            LastUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Webinar",
                            StartTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 2,
                            AllowGuests = true,
                            BannerImage = "Image",
                            Description = "Bild.png",
                            EndTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            LastUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "AW",
                            StartTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 3,
                            AllowGuests = true,
                            BannerImage = "Image",
                            Description = "Bild.png",
                            EndTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            LastUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Meet-up",
                            StartTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("AlumniNetworkAPI.Models.Domain.Group", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<bool>("IsPrivate")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.HasKey("Id");

                    b.ToTable("Groups");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "ASP.NET",
                            IsPrivate = true,
                            Name = ".Net"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Java and Javascript",
                            IsPrivate = true,
                            Name = "Java"
                        },
                        new
                        {
                            Id = 3,
                            Description = "Mikael and Erik",
                            IsPrivate = false,
                            Name = "Skövde-group"
                        },
                        new
                        {
                            Id = 4,
                            Description = "Experis GBG",
                            IsPrivate = true,
                            Name = "GbG-group"
                        },
                        new
                        {
                            Id = 5,
                            Description = "Skåne",
                            IsPrivate = false,
                            Name = "Varberg-group"
                        });
                });

            modelBuilder.Entity("AlumniNetworkAPI.Models.Domain.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AuthorId")
                        .HasColumnType("int");

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("EventId")
                        .HasColumnType("int");

                    b.Property<int?>("GroupId")
                        .HasColumnType("int");

                    b.Property<DateTime>("LastUpdated")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ParentPostId")
                        .HasColumnType("int");

                    b.Property<int?>("RecieverId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int?>("TopicId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("EventId");

                    b.HasIndex("GroupId");

                    b.HasIndex("ParentPostId");

                    b.HasIndex("RecieverId");

                    b.HasIndex("TopicId");

                    b.ToTable("Posts");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AuthorId = 1,
                            Body = "Hejsan svejsan",
                            EventId = 2,
                            GroupId = 1,
                            LastUpdated = new DateTime(2023, 3, 22, 21, 20, 50, 61, DateTimeKind.Local).AddTicks(2859),
                            RecieverId = 1,
                            Title = "Maryams Dagbok",
                            TopicId = 1
                        },
                        new
                        {
                            Id = 2,
                            AuthorId = 2,
                            Body = "Svejsan Hejsan",
                            GroupId = 2,
                            LastUpdated = new DateTime(2023, 3, 22, 21, 20, 50, 61, DateTimeKind.Local).AddTicks(2900),
                            RecieverId = 2,
                            Title = "Maryams Ica Lista",
                            TopicId = 2
                        },
                        new
                        {
                            Id = 3,
                            AuthorId = 3,
                            Body = "Hej svej",
                            GroupId = 3,
                            LastUpdated = new DateTime(2023, 3, 22, 21, 20, 50, 61, DateTimeKind.Local).AddTicks(2903),
                            RecieverId = 3,
                            Title = "Maryams Hemliga bok",
                            TopicId = 1
                        },
                        new
                        {
                            Id = 4,
                            AuthorId = 4,
                            Body = "Hemligt!",
                            GroupId = 4,
                            LastUpdated = new DateTime(2023, 3, 22, 21, 20, 50, 61, DateTimeKind.Local).AddTicks(2906),
                            RecieverId = 3,
                            Title = "Filips äventyr",
                            TopicId = 3
                        });
                });

            modelBuilder.Entity("AlumniNetworkAPI.Models.Domain.Topic", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.HasKey("Id");

                    b.ToTable("Topics");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Keycloak is an open source software product to allow single sign-on with Identity",
                            Name = "Keycloak"
                        },
                        new
                        {
                            Id = 2,
                            Description = "JavaScript is a high-level, often just-in-time compiled language that conforms to the ECMAScript standard.",
                            Name = "JavaScript"
                        },
                        new
                        {
                            Id = 3,
                            Description = "Tailwind CSS is an open source CSS framework.",
                            Name = "Tailwind"
                        });
                });

            modelBuilder.Entity("AlumniNetworkAPI.Models.Domain.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Bio")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("FunFact")
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<string>("KeycloakId")
                        .IsRequired()
                        .HasMaxLength(2147483647)
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Picture")
                        .HasMaxLength(2147483647)
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Bio = "Is from GBG",
                            FunFact = "Formula",
                            KeycloakId = "2ff40a61-2dea-418b-ad08-06aa0e0498fb",
                            Picture = "https://cdn-icons-png.flaticon.com/512/3135/3135715.png",
                            Status = "",
                            Username = "Filip"
                        },
                        new
                        {
                            Id = 2,
                            Bio = "ya",
                            FunFact = "Big Formula",
                            KeycloakId = "f428f142-cae0-4429-b846-991c67fc4d4f",
                            Picture = "https://cdn-icons-png.flaticon.com/512/3135/3135715.png",
                            Status = "",
                            Username = "Tommy"
                        },
                        new
                        {
                            Id = 3,
                            Bio = "Karate",
                            FunFact = "Artist",
                            KeycloakId = "",
                            Picture = "https://cdn-icons-png.flaticon.com/512/3135/3135715.png",
                            Status = "",
                            Username = "Maryam"
                        },
                        new
                        {
                            Id = 4,
                            Bio = "Varberg",
                            FunFact = ".NET utvecklare",
                            KeycloakId = "",
                            Picture = "https://cdn-icons-png.flaticon.com/512/3135/3135715.png",
                            Status = "",
                            Username = "TinTin"
                        },
                        new
                        {
                            Id = 5,
                            Bio = "Jag älskar skövde",
                            FunFact = "Bestämd",
                            KeycloakId = "",
                            Picture = "https://cdn-icons-png.flaticon.com/512/3135/3135715.png",
                            Status = "",
                            Username = "Mikael"
                        },
                        new
                        {
                            Id = 6,
                            Bio = "Jag älskar också skövde",
                            FunFact = "Gillar att koda",
                            KeycloakId = "",
                            Picture = "https://cdn-icons-png.flaticon.com/512/3135/3135715.png",
                            Status = "",
                            Username = "Erik"
                        },
                        new
                        {
                            Id = 7,
                            Bio = "Jag får andra att skratta",
                            FunFact = "React är min grej",
                            KeycloakId = "",
                            Picture = "https://cdn-icons-png.flaticon.com/512/3135/3135715.png",
                            Status = "",
                            Username = "Alexander"
                        },
                        new
                        {
                            Id = 8,
                            Bio = "Jag gillar promenader",
                            FunFact = "Bra på Frontend",
                            KeycloakId = "",
                            Picture = "https://cdn-icons-png.flaticon.com/512/3135/3135715.png",
                            Status = "",
                            Username = "Amanda"
                        },
                        new
                        {
                            Id = 9,
                            Bio = "Jag gillar promenader",
                            FunFact = "Bra på Frontend",
                            KeycloakId = "qweqwe",
                            Picture = "https://cdn-icons-png.flaticon.com/512/3135/3135715.png",
                            Status = "",
                            Username = "Manda"
                        });
                });

            modelBuilder.Entity("EventGroup", b =>
                {
                    b.Property<int>("GroupsId")
                        .HasColumnType("int");

                    b.Property<int>("EventsId")
                        .HasColumnType("int");

                    b.HasKey("GroupsId", "EventsId");

                    b.HasIndex("EventsId");

                    b.ToTable("EventGroup");

                    b.HasData(
                        new
                        {
                            GroupsId = 1,
                            EventsId = 1
                        },
                        new
                        {
                            GroupsId = 2,
                            EventsId = 2
                        },
                        new
                        {
                            GroupsId = 3,
                            EventsId = 3
                        },
                        new
                        {
                            GroupsId = 3,
                            EventsId = 2
                        });
                });

            modelBuilder.Entity("EventTopic", b =>
                {
                    b.Property<int>("TopicsId")
                        .HasColumnType("int");

                    b.Property<int>("EventsId")
                        .HasColumnType("int");

                    b.HasKey("TopicsId", "EventsId");

                    b.HasIndex("EventsId");

                    b.ToTable("EventTopic");

                    b.HasData(
                        new
                        {
                            TopicsId = 2,
                            EventsId = 1
                        },
                        new
                        {
                            TopicsId = 2,
                            EventsId = 3
                        },
                        new
                        {
                            TopicsId = 3,
                            EventsId = 1
                        });
                });

            modelBuilder.Entity("EventUserInvitation", b =>
                {
                    b.Property<int>("InvitedUsersId")
                        .HasColumnType("int");

                    b.Property<int>("UnrespondedEventsId")
                        .HasColumnType("int");

                    b.HasKey("InvitedUsersId", "UnrespondedEventsId");

                    b.HasIndex("UnrespondedEventsId");

                    b.ToTable("EventUserInvitation");
                });

            modelBuilder.Entity("GroupMember", b =>
                {
                    b.Property<int>("GroupId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("GroupId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("GroupMember");

                    b.HasData(
                        new
                        {
                            GroupId = 1,
                            UserId = 1
                        },
                        new
                        {
                            GroupId = 1,
                            UserId = 2
                        },
                        new
                        {
                            GroupId = 1,
                            UserId = 3
                        },
                        new
                        {
                            GroupId = 1,
                            UserId = 4
                        },
                        new
                        {
                            GroupId = 1,
                            UserId = 6
                        },
                        new
                        {
                            GroupId = 1,
                            UserId = 5
                        },
                        new
                        {
                            GroupId = 2,
                            UserId = 1
                        },
                        new
                        {
                            GroupId = 2,
                            UserId = 8
                        },
                        new
                        {
                            GroupId = 3,
                            UserId = 6
                        },
                        new
                        {
                            GroupId = 3,
                            UserId = 5
                        },
                        new
                        {
                            GroupId = 4,
                            UserId = 2
                        },
                        new
                        {
                            GroupId = 5,
                            UserId = 2
                        });
                });

            modelBuilder.Entity("RSVP", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("EventId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "EventId");

                    b.HasIndex("EventId");

                    b.ToTable("RSVP");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            EventId = 1
                        },
                        new
                        {
                            UserId = 5,
                            EventId = 1
                        },
                        new
                        {
                            UserId = 4,
                            EventId = 1
                        },
                        new
                        {
                            UserId = 2,
                            EventId = 2
                        },
                        new
                        {
                            UserId = 3,
                            EventId = 2
                        },
                        new
                        {
                            UserId = 4,
                            EventId = 2
                        },
                        new
                        {
                            UserId = 5,
                            EventId = 3
                        },
                        new
                        {
                            UserId = 1,
                            EventId = 3
                        },
                        new
                        {
                            UserId = 2,
                            EventId = 3
                        });
                });

            modelBuilder.Entity("TopicUser", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("TopicId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "TopicId");

                    b.HasIndex("TopicId");

                    b.ToTable("TopicUser");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            TopicId = 1
                        },
                        new
                        {
                            UserId = 5,
                            TopicId = 1
                        },
                        new
                        {
                            UserId = 4,
                            TopicId = 1
                        },
                        new
                        {
                            UserId = 2,
                            TopicId = 2
                        },
                        new
                        {
                            UserId = 8,
                            TopicId = 2
                        },
                        new
                        {
                            UserId = 4,
                            TopicId = 2
                        },
                        new
                        {
                            UserId = 5,
                            TopicId = 3
                        },
                        new
                        {
                            UserId = 7,
                            TopicId = 3
                        },
                        new
                        {
                            UserId = 8,
                            TopicId = 3
                        });
                });

            modelBuilder.Entity("UserEvents", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("EventId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "EventId");

                    b.HasIndex("EventId");

                    b.ToTable("UserEvents");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            EventId = 1
                        },
                        new
                        {
                            UserId = 5,
                            EventId = 1
                        },
                        new
                        {
                            UserId = 4,
                            EventId = 1
                        },
                        new
                        {
                            UserId = 2,
                            EventId = 2
                        },
                        new
                        {
                            UserId = 3,
                            EventId = 2
                        },
                        new
                        {
                            UserId = 4,
                            EventId = 2
                        },
                        new
                        {
                            UserId = 5,
                            EventId = 3
                        },
                        new
                        {
                            UserId = 1,
                            EventId = 3
                        },
                        new
                        {
                            UserId = 2,
                            EventId = 3
                        });
                });

            modelBuilder.Entity("AlumniNetworkAPI.Models.Domain.Event", b =>
                {
                    b.HasOne("AlumniNetworkAPI.Models.Domain.User", "Author")
                        .WithMany("AuthoredEvents")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Author");
                });

            modelBuilder.Entity("AlumniNetworkAPI.Models.Domain.Post", b =>
                {
                    b.HasOne("AlumniNetworkAPI.Models.Domain.User", "Author")
                        .WithMany("AuthoredPosts")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("AlumniNetworkAPI.Models.Domain.Event", "Event")
                        .WithMany("Posts")
                        .HasForeignKey("EventId");

                    b.HasOne("AlumniNetworkAPI.Models.Domain.Group", "Group")
                        .WithMany("Posts")
                        .HasForeignKey("GroupId");

                    b.HasOne("AlumniNetworkAPI.Models.Domain.Post", "ParentPost")
                        .WithMany("Replies")
                        .HasForeignKey("ParentPostId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AlumniNetworkAPI.Models.Domain.User", "Reciever")
                        .WithMany("RecievedPosts")
                        .HasForeignKey("RecieverId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AlumniNetworkAPI.Models.Domain.Topic", "Topic")
                        .WithMany("Posts")
                        .HasForeignKey("TopicId");

                    b.Navigation("Author");

                    b.Navigation("Event");

                    b.Navigation("Group");

                    b.Navigation("ParentPost");

                    b.Navigation("Reciever");

                    b.Navigation("Topic");
                });

            modelBuilder.Entity("EventGroup", b =>
                {
                    b.HasOne("AlumniNetworkAPI.Models.Domain.Event", null)
                        .WithMany()
                        .HasForeignKey("EventsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AlumniNetworkAPI.Models.Domain.Group", null)
                        .WithMany()
                        .HasForeignKey("GroupsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EventTopic", b =>
                {
                    b.HasOne("AlumniNetworkAPI.Models.Domain.Event", null)
                        .WithMany()
                        .HasForeignKey("EventsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AlumniNetworkAPI.Models.Domain.Topic", null)
                        .WithMany()
                        .HasForeignKey("TopicsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EventUserInvitation", b =>
                {
                    b.HasOne("AlumniNetworkAPI.Models.Domain.User", null)
                        .WithMany()
                        .HasForeignKey("InvitedUsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AlumniNetworkAPI.Models.Domain.Event", null)
                        .WithMany()
                        .HasForeignKey("UnrespondedEventsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GroupMember", b =>
                {
                    b.HasOne("AlumniNetworkAPI.Models.Domain.Group", null)
                        .WithMany()
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AlumniNetworkAPI.Models.Domain.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RSVP", b =>
                {
                    b.HasOne("AlumniNetworkAPI.Models.Domain.Event", null)
                        .WithMany()
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AlumniNetworkAPI.Models.Domain.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TopicUser", b =>
                {
                    b.HasOne("AlumniNetworkAPI.Models.Domain.Topic", null)
                        .WithMany()
                        .HasForeignKey("TopicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AlumniNetworkAPI.Models.Domain.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("UserEvents", b =>
                {
                    b.HasOne("AlumniNetworkAPI.Models.Domain.Event", null)
                        .WithMany()
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AlumniNetworkAPI.Models.Domain.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AlumniNetworkAPI.Models.Domain.Event", b =>
                {
                    b.Navigation("Posts");
                });

            modelBuilder.Entity("AlumniNetworkAPI.Models.Domain.Group", b =>
                {
                    b.Navigation("Posts");
                });

            modelBuilder.Entity("AlumniNetworkAPI.Models.Domain.Post", b =>
                {
                    b.Navigation("Replies");
                });

            modelBuilder.Entity("AlumniNetworkAPI.Models.Domain.Topic", b =>
                {
                    b.Navigation("Posts");
                });

            modelBuilder.Entity("AlumniNetworkAPI.Models.Domain.User", b =>
                {
                    b.Navigation("AuthoredEvents");

                    b.Navigation("AuthoredPosts");

                    b.Navigation("RecievedPosts");
                });
#pragma warning restore 612, 618
        }
    }
}
