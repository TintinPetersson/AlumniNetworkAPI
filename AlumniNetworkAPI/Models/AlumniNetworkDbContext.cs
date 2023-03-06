using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;

namespace AlumniNetworkAPI.Models
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
            .WithMany(e => e.AcceptedUsers)
            .UsingEntity<Dictionary<string, object>>("RSVP");

            modelBuilder.Entity<User>()
            .HasMany(e => e.UnrespondedEvents)
            .WithMany(e => e.InvitedUsers)
            .UsingEntity<Dictionary<string, object>>("EventUserInvitation");


            modelBuilder.Entity<User>()
            .HasMany(e => e.Groups)
            .WithMany(e => e.Users)
            .UsingEntity<Dictionary<string, object>>("GroupMember");
        }
    }
}