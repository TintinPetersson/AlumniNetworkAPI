using AlumniNetworkAPI.Models.Domain;
using AlumniNetworkAPI.Models.Dtos.Events;
using AlumniNetworkAPI.Models.Dtos.Posts;
using AlumniNetworkAPI.Models.Dtos.Users;

namespace AlumniNetworkAPI.Models.Dtos.Groups
{
    public class GroupReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public bool IsPrivate { get; set; }

        public virtual ICollection<EventGroupReadDto>? Events { get; set; } //Many-to-many
        public virtual ICollection<PostGroupReadDTO>? Posts { get; set; } //One-to-many
        public virtual ICollection<UserReadDto>? Users { get; set; } //Many-to-many
    }
}
