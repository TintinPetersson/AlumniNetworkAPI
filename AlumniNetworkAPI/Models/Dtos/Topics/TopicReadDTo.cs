using AlumniNetworkAPI.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace AlumniNetworkAPI.Models.Dtos.Topics
{
    public class TopicReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

        public virtual ICollection<Event>? Events { get; set; }
        public virtual ICollection<User>? Users { get; set; }
        public virtual ICollection<Post>? Posts { get; set; }
    }
}
