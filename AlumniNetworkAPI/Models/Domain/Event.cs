using System.ComponentModel.DataAnnotations;

namespace AlumniNetworkAPI.Models.Domain
{
    public class Event
    {
        public int Id { get; set; }
        public DateTime LastUpdated { get; set; }
        [MaxLength(40)]
        [Required]
        public string Name { get; set; } = string.Empty;
        [MaxLength(200)]
        public string? Description { get; set; }
        [Required]
        public bool AllowGuests { get; set; }
        public string? BannerImage { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public virtual ICollection<Post>? Posts { get; set; } //One-to-many
        public virtual ICollection<Group>? Groups { get; set; } //Many-to-many
        public virtual ICollection<User>? InvitedUsers { get; set; } //Many-to-many
        public virtual ICollection<User>? AcceptedUsers { get; set; } //Many-to-many
        public virtual ICollection<Topic>? Topics { get; set; } //Many-to-many
        public int? AuthorId { get; set; }
        public User? Author { get; set; } //One-Many
    }
}