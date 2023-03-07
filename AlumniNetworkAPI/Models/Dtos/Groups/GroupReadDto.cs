using AlumniNetworkAPI.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace AlumniNetworkAPI.Models.Dtos.Groups
{
    public class GroupReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public bool IsPrivate { get; set; }

        public virtual ICollection<Event>? Events { get; set; } //Many-to-many
        public virtual ICollection<Post>? Posts { get; set; } //One-to-many
        public virtual ICollection<User>? Users { get; set; } //Many-to-many
    }
}
