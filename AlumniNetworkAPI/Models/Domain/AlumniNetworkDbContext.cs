using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;

namespace AlumniNetworkAPI.Models.Domain
{
    public class AlumniNetworkDbContext : DbContext
    {
        public AlumniNetworkDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Event> Events { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Relationships
            modelBuilder.Entity<Post>()
            .HasMany(e => e.Replies)
            .WithOne(e => e.ParentPost)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
            .HasMany(e => e.AuthoredPosts)
            .WithOne(e => e.Author)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
            .HasMany(e => e.RecievedPosts)
            .WithOne(e => e.Reciever)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
            .HasMany(e => e.AcceptedEvents)
            .WithMany(u => u.AcceptedUsers)
            .UsingEntity<Dictionary<string, object>>(
                "RSVP",
                e => e.HasOne<Event>().WithMany().HasForeignKey("EventId"),
                u => u.HasOne<User>().WithMany().HasForeignKey("UserId"),
                joinEntity =>
                {
                    joinEntity.HasKey("UserId", "EventId");
                    joinEntity.HasData(
                        new { UserId = 1, EventId = 1 },
                        new { UserId = 5, EventId = 1 },
                        new { UserId = 4, EventId = 1 },

                        new { UserId = 2, EventId = 2 },
                        new { UserId = 3, EventId = 2 },
                        new { UserId = 4, EventId = 2 },

                        new { UserId = 5, EventId = 3 },
                        new { UserId = 1, EventId = 3 },
                        new { UserId = 2, EventId = 3 }
                        );
                });

            modelBuilder.Entity<User>()
            .HasMany(e => e.UnrespondedEvents)
            .WithMany(e => e.InvitedUsers)
            .UsingEntity<Dictionary<string, object>>("EventUserInvitation");


            modelBuilder.Entity<User>()
            .HasMany(e => e.Groups)
            .WithMany(e => e.Users)
            .UsingEntity<Dictionary<string, object>>("GroupMember");

            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, KeycloakId = "2ff40a61-2dea-418b-ad08-06aa0e0498fb", Username = "Filip", Picture = "Bild.png", Status = "", Bio = "Is from GBG", FunFact = "Formula" },
                new User { Id = 2, KeycloakId = "f428f142-cae0-4429-b846-991c67fc4d4f", Username = "admin", Picture = "", Status = "", Bio = "", FunFact = "Big Formula" },
                new User { Id = 3, KeycloakId = "", Username = "Maryam", Picture = "Bild3.png", Status = "", Bio = "Karate", FunFact = "Artist" },
                new User { Id = 4, KeycloakId = "", Username = "TinTin", Picture = "Bild4.png", Status = "", Bio = "Varberg", FunFact = ".NET utvecklare" },
                new User { Id = 5, KeycloakId = "", Username = "Annika", Picture = "Bild5.png", Status = "", Bio = "Älska, älska inte", FunFact = "Bestämd" }
                );

            modelBuilder.Entity<Event>().HasData(
               new Event { Id = 1, Name = "Webinar", Description = "Bild.png", AllowGuests = true, BannerImage = "Image" },
               new Event { Id = 2, Name = "AW", Description = "Bild.png", AllowGuests = true, BannerImage = "Image" },
               new Event { Id = 3, Name = "Meet-up", Description = "Bild.png", AllowGuests = true, BannerImage = "Image" }

               );
            modelBuilder.Entity<User>()
                .HasMany(User => User.UnrespondedEvents)
                .WithMany(Event => Event.InvitedUsers)
                .UsingEntity<Dictionary<string, object>>(
                "UserEvents",
                left => left.HasOne<Event>().WithMany().HasForeignKey("EventId")!,
                right => right.HasOne<User>().WithMany().HasForeignKey("UserId"),
                joinEntity =>
                {
                    joinEntity.HasKey("UserId", "EventId");
                    joinEntity.HasData(
                    new { UserId = 1, EventId = 1 },
                    new { UserId = 5, EventId = 1 },
                    new { UserId = 4, EventId = 1 },

                    new { UserId = 2, EventId = 2 },
                    new { UserId = 3, EventId = 2 },
                    new { UserId = 4, EventId = 2 },

                    new { UserId = 5, EventId = 3 },
                    new { UserId = 1, EventId = 3 },
                    new { UserId = 2, EventId = 3 }

                    );


                });



        }
    }
}