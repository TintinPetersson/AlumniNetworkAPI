using AlumniNetworkAPI.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace AlumniNetworkAPI.Models.Dtos.Events
{
    public class EventReadDto
    {
        public int Id { get; set; }
        public DateTime LastUpdated { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool AllowGuests { get; set; }
        public string? BannerImage { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public virtual ICollection<Post>? Posts { get; set; } //One-to-many
        public virtual ICollection<Group>? Groups { get; set; } //Many-to-many
        public virtual ICollection<User>? InvitedUsers { get; set; } //Many-to-many
        public virtual ICollection<User>? AcceptedUsers { get; set; } //Many-to-many
        public virtual ICollection<Topic>? Topics { get; set; } //Many-to-many
    }
}
