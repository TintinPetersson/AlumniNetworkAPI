using AlumniNetworkAPI.Models.Domain;
using AlumniNetworkAPI.Models.Dtos.Groups;
using AlumniNetworkAPI.Models.Dtos.Posts;
using AlumniNetworkAPI.Models.Dtos.Topics;
using AlumniNetworkAPI.Models.Dtos.Users;
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

        public virtual ICollection<PostEventReadDto>? Posts { get; set; } //One-to-many
        public virtual ICollection<GroupUserReadDTO>? Groups { get; set; } //Many-to-many
        public virtual ICollection<UserReadDto>? InvitedUsers { get; set; } //Many-to-many
        public virtual ICollection<UserReadDto>? AcceptedUsers { get; set; } //Many-to-many
        //public virtual ICollection<TopicReadDto>? Topics { get; set; } //Many-to-many
    }
}
