using AlumniNetworkAPI.Models.Domain;
using AlumniNetworkAPI.Models.Dtos.Events;
using AlumniNetworkAPI.Models.Dtos.Posts;
using AlumniNetworkAPI.Models.Dtos.Users;
using System.ComponentModel.DataAnnotations;

namespace AlumniNetworkAPI.Models.Dtos.Topics
{
    public class TopicReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

        public virtual ICollection<EventGroupReadDto>? Events { get; set; }
        public virtual ICollection<UserPostReadDto>? Users { get; set; }
        public virtual ICollection<PostGroupReadDTO>? Posts { get; set; }
    }
}
