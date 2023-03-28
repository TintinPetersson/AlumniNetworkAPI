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
                new User { Id = 1, KeycloakId = "7a2a4108-21b7-4a7b-9e15-415262ef547d", Username = "John", Status = "Working", Bio = "Software engineer by profession. Love to play football in free time.", FunFact = "I can solve a Rubik's cube in under a minute." },
                new User { Id = 2, KeycloakId = "f428f142-cae0-4429-b846-991c67fc4d4f", Username = "Jane", Status = "Working", Bio = "Marketing professional who loves to read and travel.", FunFact = "I have visited 10 countries in the last year." },
                new User { Id = 3, KeycloakId = "5da8c3d6-3edc-4ff4-9f80-d75a35ca470c", Username = "Alice", Status = "Student", Bio = "Computer science major at Chalmers University. Enjoys playing video games and watching sci-fi movies.", FunFact = "I have won several programming competitions." },
                new User { Id = 4, KeycloakId = "82070311-b1ca-4828-bb68-9a62b957361e", Username = "Bob", Status = "Student", Bio = "Studying psychology at Yale University. Passionate about music and plays the guitar.", FunFact = "I can play the guitar upside down and backwards." }, 
                new User { Id = 5, KeycloakId = "4a7663b6-6940-46c3-86ef-659f014ae8d1", Username = "Sarah", Status = "Working", Bio = "Accountant by profession. Loves to hike and explore nature.", FunFact = "I have climbed Mount Kilimanjaro." },
                new User { Id = 6, KeycloakId = "b6781ded-8de2-416f-b03b-0fade74353fe", Username = "David", Status = "Working", Bio = "Sales executive who enjoys playing basketball and cooking.", FunFact = "I have cooked for a Michelin-starred chef." },
                new User { Id = 7, KeycloakId = "1cf5b92c-a998-41e0-9ad2-d7c2ff1c2673", Username = "Emily", Status = "Student", Bio = "Studying medicine at LMN University. Enjoys practicing yoga and volunteering at a local hospital.", FunFact = "I can hold a handstand for over a minute." },
                new User { Id = 8, KeycloakId = "cd42f358-b591-4f7b-a6d3-4bd8e0b840af", Username = "Chris", Status = "Student", Bio = "Studying finance at PQR College. Enjoys playing tennis and watching documentaries.", FunFact = "I have won several tennis tournaments in my city." },
                new User { Id = 9, KeycloakId = "f0b6c858-24bc-4d03-b89e-6269f8c43241", Username = "Avery", Status = "Working", Bio = "Graphic designer by profession. Enjoys painting and hiking on weekends.", FunFact = "I have exhibited my paintings in several art galleries." }
            );

            modelBuilder.Entity<Topic>().HasData(
                new Topic { Id = 1, Name = "Engineering", Description = "Topics related to engineering, including mechanical, electrical, civil, and chemical engineering." },
                new Topic { Id = 2, Name = "Law", Description = "Topics related to law, including civil law, criminal law, international law, and human rights." },
                new Topic { Id = 3, Name = "Business", Description = "Topics related to business, including accounting, finance, marketing, and management." },
                new Topic { Id = 4, Name = "Medicine", Description = "Topics related to medicine, including anatomy, physiology, pharmacology, and pathology." },
                new Topic { Id = 5, Name = "Education", Description = "Topics related to education, including teaching methods, curriculum design, educational psychology, and special education." }
            );


            modelBuilder.Entity<Group>().HasData(
                new Group { Id = 1, Name = "Alumni Sports Club", Description = "A group for alumni who are interested in sports and fitness.", IsPrivate = true },
                new Group { Id = 2, Name = "Business Networking Group", Description = "A group for alumni who are interested in business networking and entrepreneurship.", IsPrivate = false },
                new Group { Id = 3, Name = "Engineering Alumni Group", Description = "A group for alumni who studied engineering at the university.", IsPrivate = false },
                new Group { Id = 4, Name = "Law School Alumni Association", Description = "An association for all alumni of the law school.", IsPrivate = false },
                new Group { Id = 5, Name = "Medical Alumni Association", Description = "An association for all alumni of the medical school.", IsPrivate = false },
                new Group { Id = 6, Name = "Alumni Book Club", Description = "A group for alumni who love to read and discuss books.", IsPrivate = true },
                new Group { Id = 7, Name = "Alumni Travel Group", Description = "A group for alumni who love to travel and explore new places.", IsPrivate = true }
            );

            modelBuilder.Entity<Event>().HasData(
                new Event { Id = 1, Name = "Introduction to Machine Learning", Description = "Join us for an online workshop on the basics of machine learning. Learn about various algorithms and techniques used in machine learning.", AllowGuests = true, BannerImage = "machinelearning.jpg", StartTime = new DateTime(2023, 4, 5, 10, 0, 0), EndTime = new DateTime(2023, 4, 5, 18, 0, 0) },
                new Event { Id = 2, Name = "Alumni Networking Mixer", Description = "Join us for an evening of networking and socializing with fellow alumni. Make new connections, catch up with old friends, and expand your professional network.", AllowGuests = true, BannerImage = "networking.jpg", StartTime = new DateTime(2023, 4, 10, 10, 0, 0), EndTime = new DateTime(2023, 4, 10, 18, 0, 0) },
                new Event { Id = 3, Name = "Community Service Project", Description = "Join us for a community service project to give back to the community. Help us clean up a local park and make it a better place for everyone.", AllowGuests = true, BannerImage = "communityservice.jpg", StartTime = new DateTime(2023, 3, 15, 10, 0, 0), EndTime = new DateTime(2023, 4, 15, 18, 0, 0) },
                new Event { Id = 4, Name = "Alumni Homecoming 2023", Description = "Join us for the annual alumni homecoming event. Reconnect with old friends, meet new ones, and celebrate the university's legacy.", AllowGuests = true, BannerImage = "homecoming.jpg", StartTime = new DateTime(2023, 4, 20, 10, 0, 0), EndTime = new DateTime(2023, 4, 20, 18, 0, 0) },
                new Event { Id = 5, Name = "Career Fair 2023", Description = "Looking for new job opportunities? Join us for the annual career fair, where you can network with employers and learn about job openings.", AllowGuests = false, BannerImage = "careerfair.jpg", StartTime = new DateTime(2023, 4, 25, 10, 0, 0), EndTime = new DateTime(2023, 4, 25, 18, 0, 0) }
            );


            modelBuilder.Entity<Post>().HasData(
                new Post { Id = 1, Title = "Tips for Electrical Engineering Students", Body = "As an electrical engineering major, I have learned a lot during my time in college. Here are some tips for success: 1. Stay organized and manage your time well. 2. Take advantage of networking opportunities. 3. Get hands-on experience through internships or research. 4. Stay up-to-date with the latest technologies and trends. Good luck!", LastUpdated = DateTime.Now, AuthorId = 1, TopicId = 1, GroupId = 1, EventId = 1 },
                new Post { Id = 2, Title = "Preparing for the Bar Exam", Body = "If you're planning to take the bar exam, it's important to start preparing early. Here are some tips: 1. Create a study schedule and stick to it. 2. Take practice exams to get familiar with the format and types of questions. 3. Join a study group to stay motivated and get support. 4. Take care of yourself and manage your stress. You can do it!", LastUpdated = DateTime.Now, AuthorId = 2, TopicId = 2, GroupId = 2, EventId = 2 },
                new Post { Id = 3, Title = "Tips for Starting a Small Business", Body = "Starting a small business can be challenging, but also very rewarding. Here are some tips to help you get started: 1. Conduct market research to identify a need for your product or service. 2. Create a business plan and set clear goals. 3. Secure funding and resources. 4. Build a strong team and culture. Good luck on your entrepreneurial journey!", LastUpdated = DateTime.Now, AuthorId = 3, TopicId = 3, GroupId = 3, EventId = 3 },
                new Post { Id = 4, Title = "My Experience as a Medical Resident", Body = "As a medical resident, I have had many challenges and opportunities for growth. Here are some things I have learned: 1. Time management is key. 2. Communication skills are critical. 3. Self-care is important to avoid burnout. 4. Learning is a lifelong process. Best of luck to all medical residents!", LastUpdated = DateTime.Now, AuthorId = 4, TopicId = 4, GroupId = 4, EventId = 1 },
                new Post { Id = 5, Title = "The Importance of STEM Education", Body = "STEM education is crucial for the future of our society and economy. Here are some reasons why: 1. STEM careers are in high demand and offer high salaries. 2. STEM skills are needed for innovation and problem-solving. 3. STEM education can promote diversity and social equality. Let's encourage more young people to pursue STEM education!", LastUpdated = DateTime.Now, AuthorId = 5, TopicId = 1, GroupId = 1, EventId = 4 }
                
            );

            //Relationships
            modelBuilder.Entity<Post>()
            .HasMany(e => e.Replies)
            .WithOne(e => e.Parent)
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
                    new { UserId = 2, EventId = 3 },

                    new { UserId = 3, EventId = 4 },
                    new { UserId = 6, EventId = 4 },
                    new { UserId = 7, EventId = 4 },

                    new { UserId = 8, EventId = 5 },
                    new { UserId = 9, EventId = 5 },
                    new { UserId = 1, EventId = 5 }
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

                            new { UserId = 4, GroupId = 2 },
                            new { UserId = 8, GroupId = 2 },

                            new { UserId = 6, GroupId = 3 },
                            new { UserId = 5, GroupId = 3 },

                            new { UserId = 2, GroupId = 4 },

                            new { UserId = 2, GroupId = 5 },
                            new { UserId = 4, GroupId = 5 },
                            
                            new { UserId = 6, GroupId = 6 },
                            new { UserId = 3, GroupId = 6 },
                            
                            new { UserId = 8, GroupId = 7 },
                            new { UserId = 9, GroupId = 7 }
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
                  new { UserId = 4, TopicId = 2 },
                  new { UserId = 9, TopicId = 3 },
                  new { UserId = 5, TopicId = 3 },
                  new { UserId = 7, TopicId = 4 },
                  new { UserId = 8, TopicId = 5 }
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
                    new { GroupsId = 3, EventsId = 2 },
                    new { GroupsId = 5, EventsId = 4 },
                    new { GroupsId = 6, EventsId = 5 }
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
                        new { TopicsId = 5, EventsId = 2 },
                        new { TopicsId = 3, EventsId = 3 },
                        new { TopicsId = 4, EventsId = 4 },
                        new { TopicsId = 2, EventsId = 5 },
                        new { TopicsId = 4, EventsId = 5 }
                    );
                });


        }
    }
}

