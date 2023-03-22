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
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, KeycloakId = "2ff40a61-2dea-418b-ad08-06aa0e0498fb", Username = "Filip", Status = "", Bio = "Is from GBG", FunFact = "Formula" },
                new User { Id = 2, KeycloakId = "f428f142-cae0-4429-b846-991c67fc4d4f", Username = "Tommy", Status = "", Bio = "ya", FunFact = "Big Formula" },
                new User { Id = 3, KeycloakId = "", Username = "Maryam", Status = "", Bio = "Karate", FunFact = "Artist" },
                new User { Id = 4, KeycloakId = "", Username = "TinTin", Status = "", Bio = "Varberg", FunFact = ".NET utvecklare" },
                new User { Id = 5, KeycloakId = "", Username = "Mikael", Status = "", Bio = "Jag älskar skövde", FunFact = "Bestämd" },
                new User { Id = 6, KeycloakId = "", Username = "Erik", Status = "", Bio = "Jag älskar också skövde", FunFact = "Gillar att koda" },
                new User { Id = 7, KeycloakId = "", Username = "Alexander", Status = "", Bio = "Jag får andra att skratta", FunFact = "React är min grej" },
                new User { Id = 8, KeycloakId = "", Username = "Amanda", Status = "", Bio = "Jag gillar promenader", FunFact = "Bra på Frontend" },
                new User { Id = 9, KeycloakId = "qweqwe", Username = "Manda", Status = "", Bio = "Jag gillar promenader", FunFact = "Bra på Frontend" }
            );

            modelBuilder.Entity<Topic>().HasData(
                new Topic { Id = 1, Name = "Keycloak", Description = "Keycloak is an open source software product to allow single sign-on with Identity" },
                new Topic { Id = 2, Name = "JavaScript", Description = "JavaScript is a high-level, often just-in-time compiled language that conforms to the ECMAScript standard." },
                new Topic { Id = 3, Name = "Tailwind", Description = "Tailwind CSS is an open source CSS framework." }
            );

            modelBuilder.Entity<Post>().HasData(
                new Post { Id = 1, Title = "Maryams Dagbok", Body = "Hejsan svejsan", LastUpdated = DateTime.Now, AuthorId = 1, TopicId = 1, GroupId = 1, EventId = 2 ,RecieverId = 1 },
                new Post { Id = 2, Title = "Maryams Ica Lista", Body = "Svejsan Hejsan", LastUpdated = DateTime.Now, AuthorId = 2, TopicId = 2, GroupId = 2, RecieverId = 2 },
                new Post { Id = 3, Title = "Maryams Hemliga bok", Body = "Hej svej", LastUpdated = DateTime.Now, AuthorId = 3, TopicId = 1, GroupId = 3 , RecieverId = 3 },
                 new Post { Id = 4, Title = "Filips äventyr", Body = "Hemligt!", LastUpdated = DateTime.Now, AuthorId = 4, TopicId = 3, GroupId = 4, RecieverId = 3 }
            );


            modelBuilder.Entity<Group>().HasData(
                new Group { Id = 1, Name = ".Net", Description = "ASP.NET", IsPrivate = true },
                new Group { Id = 2, Name = "Java", Description = "Java and Javascript", IsPrivate = true },
                new Group { Id = 3, Name = "Skövde-group", Description = "Mikael and Erik", IsPrivate = false },
                new Group { Id = 4, Name = "GbG-group", Description = "Experis GBG", IsPrivate = true },
                new Group { Id = 5, Name = "Varberg-group", Description = "Skåne", IsPrivate = false }
            );

            modelBuilder.Entity<Event>().HasData(
               new Event { Id = 1, Name = "Webinar", Description = "Bild.png", AllowGuests = true, BannerImage = "Image" },
               new Event { Id = 2, Name = "AW", Description = "Bild.png", AllowGuests = true, BannerImage = "Image" },
               new Event { Id = 3, Name = "Meet-up", Description = "Bild.png", AllowGuests = true, BannerImage = "Image" }

            );


            //Relationships
            modelBuilder.Entity<Post>()
            .HasMany(e => e.Replies)
            .WithOne(e => e.ParentPost)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Post>()
            .HasOne(p => p.Group)
            .WithMany(g => g.Posts)
            .HasForeignKey(p => p.GroupId);

            modelBuilder.Entity<Post>()
            .HasOne(p => p.Event)
            .WithMany(g => g.Posts)
            .HasForeignKey(p => p.EventId);

            modelBuilder.Entity<User>()
            .HasMany(e => e.AuthoredEvents)
            .WithOne(e => e.Author)
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

            modelBuilder.Entity<User>()
                .HasMany(e => e.Groups)
                .WithMany(e => e.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "GroupMember",
                    e => e.HasOne<Group>().WithMany().HasForeignKey("GroupId")!,
                    e => e.HasOne<User>().WithMany().HasForeignKey("UserId"),
                    joinEntity =>
                    {
                        joinEntity.HasKey("GroupId", "UserId");
                        joinEntity.HasData(
                            new { UserId = 1, GroupId = 1 },
                            new { UserId = 2, GroupId = 1 },
                            new { UserId = 3, GroupId = 1 },
                            new { UserId = 4, GroupId = 1 },
                            new { UserId = 6, GroupId = 1 },
                            new { UserId = 5, GroupId = 1 },

                            new { UserId = 1, GroupId = 2 },
                            new { UserId = 8, GroupId = 2 },

                            new { UserId = 6, GroupId = 3 },
                            new { UserId = 5, GroupId = 3 },

                            new { UserId = 2, GroupId = 4 },

                            new { UserId = 2, GroupId = 5 }
                            );
                    });


            modelBuilder.Entity<User>()
              .HasMany(u => u.Topics)
              .WithMany(e => e.Users)
              .UsingEntity<Dictionary<string, object>>(
              "TopicUser",
              e => e.HasOne<Topic>().WithMany().HasForeignKey("TopicId")!,
              e => e.HasOne<User>().WithMany().HasForeignKey("UserId"),
              joinEntity =>
              {
                  joinEntity.HasKey("UserId", "TopicId");
                  joinEntity.HasData(
                  new { UserId = 1, TopicId = 1 },
                  new { UserId = 5, TopicId = 1 },
                  new { UserId = 4, TopicId = 1 },
                  new { UserId = 2, TopicId = 2 },
                  new { UserId = 8, TopicId = 2 },
                  new { UserId = 4, TopicId = 2 },
                  new { UserId = 5, TopicId = 3 },
                  new { UserId = 7, TopicId = 3 },
                  new { UserId = 8, TopicId = 3 }
                  );
              });

            modelBuilder.Entity<Event>()
                .HasMany(e => e.Groups)
                .WithMany(e => e.Events)
                .UsingEntity<Dictionary<string, object>>(
                        "EventGroup",
                r => r.HasOne<Group>().WithMany().HasForeignKey("GroupsId"),
                l => l.HasOne<Event>().WithMany().HasForeignKey("EventsId"),
                je =>
            {
                je.HasKey("GroupsId", "EventsId");
                je.HasData(
                    new { GroupsId = 1, EventsId = 1 },
                    new { GroupsId = 2, EventsId = 2 },
                    new { GroupsId = 3, EventsId = 3 },
                    new { GroupsId = 3, EventsId = 2 }
                    );
            });

            modelBuilder.Entity<Event>()
            .HasMany(e => e.Topics)
            .WithMany(e => e.Events)
            .UsingEntity<Dictionary<string, object>>(
                "EventTopic",
                r => r.HasOne<Topic>().WithMany().HasForeignKey("TopicsId"),
                l => l.HasOne<Event>().WithMany().HasForeignKey("EventsId"),
                je =>
                {
                    je.HasKey("TopicsId", "EventsId");
                    je.HasData(
                        new { TopicsId = 2, EventsId = 1 },
                        new { TopicsId = 2, EventsId = 3 },
                        new { TopicsId = 3, EventsId = 1 }
                    );
                });


        }
    }
}

