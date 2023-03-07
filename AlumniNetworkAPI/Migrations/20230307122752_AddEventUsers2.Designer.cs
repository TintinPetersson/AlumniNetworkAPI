﻿// <auto-generated />
using System;
using AlumniNetworkAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AlumniNetworkAPI.Migrations
{
    [DbContext(typeof(AlumniNetworkDbContext))]
    [Migration("20230307122752_AddEventUsers2")]
    partial class AddEventUsers2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AlumniNetworkAPI.Models.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("AllowGuests")
                        .HasColumnType("bit");

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

            modelBuilder.Entity("AlumniNetworkAPI.Models.Group", b =>
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
                });

            modelBuilder.Entity("AlumniNetworkAPI.Models.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AuthorId")
                        .HasColumnType("int");

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int?>("EventId")
                        .HasColumnType("int");

                    b.Property<int?>("GroupId")
                        .HasColumnType("int");

                    b.Property<DateTime>("LastUpdated")
                        .HasColumnType("datetime2");

                    b.Property<int>("ParentPostId")
                        .HasColumnType("int");

                    b.Property<int?>("RecieverId")
                        .HasColumnType("int");

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
                });

            modelBuilder.Entity("AlumniNetworkAPI.Models.Topic", b =>
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
                });

            modelBuilder.Entity("AlumniNetworkAPI.Models.User", b =>
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
                            Picture = "Bild.png",
                            Status = "",
                            Username = "Filip"
                        },
                        new
                        {
                            Id = 2,
                            Bio = "ya",
                            FunFact = "Big Formula",
                            KeycloakId = "",
                            Picture = "Bild2.png",
                            Status = "",
                            Username = "Tommy"
                        },
                        new
                        {
                            Id = 3,
                            Bio = "Karate",
                            FunFact = "Artist",
                            KeycloakId = "",
                            Picture = "Bild3.png",
                            Status = "",
                            Username = "Maryam"
                        },
                        new
                        {
                            Id = 4,
                            Bio = "Varberg",
                            FunFact = ".NET utvecklare",
                            KeycloakId = "",
                            Picture = "Bild4.png",
                            Status = "",
                            Username = "TinTin"
                        },
                        new
                        {
                            Id = 5,
                            Bio = "Älska, älska inte",
                            FunFact = "Bestämd",
                            KeycloakId = "",
                            Picture = "Bild5.png",
                            Status = "",
                            Username = "Annika"
                        });
                });

            modelBuilder.Entity("EventGroup", b =>
                {
                    b.Property<int>("EventsId")
                        .HasColumnType("int");

                    b.Property<int>("GroupsId")
                        .HasColumnType("int");

                    b.HasKey("EventsId", "GroupsId");

                    b.HasIndex("GroupsId");

                    b.ToTable("EventGroup");
                });

            modelBuilder.Entity("EventTopic", b =>
                {
                    b.Property<int>("EventsId")
                        .HasColumnType("int");

                    b.Property<int>("TopicsId")
                        .HasColumnType("int");

                    b.HasKey("EventsId", "TopicsId");

                    b.HasIndex("TopicsId");

                    b.ToTable("EventTopic");
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
                    b.Property<int>("GroupsId")
                        .HasColumnType("int");

                    b.Property<int>("UsersId")
                        .HasColumnType("int");

                    b.HasKey("GroupsId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("GroupMember");
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
                    b.Property<int>("TopicsId")
                        .HasColumnType("int");

                    b.Property<int>("UsersId")
                        .HasColumnType("int");

                    b.HasKey("TopicsId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("TopicUser");
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

            modelBuilder.Entity("AlumniNetworkAPI.Models.Post", b =>
                {
                    b.HasOne("AlumniNetworkAPI.Models.User", "Author")
                        .WithMany("AuthoredPosts")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("AlumniNetworkAPI.Models.Event", "Event")
                        .WithMany("Posts")
                        .HasForeignKey("EventId");

                    b.HasOne("AlumniNetworkAPI.Models.Group", "Group")
                        .WithMany("Posts")
                        .HasForeignKey("GroupId");

                    b.HasOne("AlumniNetworkAPI.Models.Post", "ParentPost")
                        .WithMany("Replies")
                        .HasForeignKey("ParentPostId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("AlumniNetworkAPI.Models.User", "Reciever")
                        .WithMany("RecievedPosts")
                        .HasForeignKey("RecieverId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AlumniNetworkAPI.Models.Topic", "Topic")
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
                    b.HasOne("AlumniNetworkAPI.Models.Event", null)
                        .WithMany()
                        .HasForeignKey("EventsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AlumniNetworkAPI.Models.Group", null)
                        .WithMany()
                        .HasForeignKey("GroupsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EventTopic", b =>
                {
                    b.HasOne("AlumniNetworkAPI.Models.Event", null)
                        .WithMany()
                        .HasForeignKey("EventsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AlumniNetworkAPI.Models.Topic", null)
                        .WithMany()
                        .HasForeignKey("TopicsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EventUserInvitation", b =>
                {
                    b.HasOne("AlumniNetworkAPI.Models.User", null)
                        .WithMany()
                        .HasForeignKey("InvitedUsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AlumniNetworkAPI.Models.Event", null)
                        .WithMany()
                        .HasForeignKey("UnrespondedEventsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GroupMember", b =>
                {
                    b.HasOne("AlumniNetworkAPI.Models.Group", null)
                        .WithMany()
                        .HasForeignKey("GroupsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AlumniNetworkAPI.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RSVP", b =>
                {
                    b.HasOne("AlumniNetworkAPI.Models.Event", null)
                        .WithMany()
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AlumniNetworkAPI.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TopicUser", b =>
                {
                    b.HasOne("AlumniNetworkAPI.Models.Topic", null)
                        .WithMany()
                        .HasForeignKey("TopicsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AlumniNetworkAPI.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("UserEvents", b =>
                {
                    b.HasOne("AlumniNetworkAPI.Models.Event", null)
                        .WithMany()
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AlumniNetworkAPI.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AlumniNetworkAPI.Models.Event", b =>
                {
                    b.Navigation("Posts");
                });

            modelBuilder.Entity("AlumniNetworkAPI.Models.Group", b =>
                {
                    b.Navigation("Posts");
                });

            modelBuilder.Entity("AlumniNetworkAPI.Models.Post", b =>
                {
                    b.Navigation("Replies");
                });

            modelBuilder.Entity("AlumniNetworkAPI.Models.Topic", b =>
                {
                    b.Navigation("Posts");
                });

            modelBuilder.Entity("AlumniNetworkAPI.Models.User", b =>
                {
                    b.Navigation("AuthoredPosts");

                    b.Navigation("RecievedPosts");
                });
#pragma warning restore 612, 618
        }
    }
}
