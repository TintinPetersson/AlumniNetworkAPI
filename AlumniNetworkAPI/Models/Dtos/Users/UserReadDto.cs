using AlumniNetworkAPI.Models.Domain;
using AlumniNetworkAPI.Models.Dtos.Events;
using AlumniNetworkAPI.Models.Dtos.Groups;
using AlumniNetworkAPI.Models.Dtos.Posts;
using AlumniNetworkAPI.Models.Dtos.Topics;

namespace AlumniNetworkAPI.Models.Dtos.Users
{
    public class UserReadDto
    {
        public int id { get; set; }
        public string? Username { get; set; }
        public string? Picture { get; set; }
        public string? Status { get; set; }
        public string? Bio { get; set; }
        public string? FunFact { get; set; }

        public virtual ICollection<TopicReadDto>? Topics { get; set; }
        public virtual ICollection<GroupReadDto>? Groups { get; set; }
        public virtual ICollection<EventReadDto>? AcceptedEvents { get; set; }
        public virtual ICollection<EventReadDto>? UnrespondedEvents { get; set; }
        public virtual ICollection<PostReadDto>? AuthoredPosts { get; set; }
        public virtual ICollection<PostReadDto>? RecievedPosts { get; set; }
    }
}
