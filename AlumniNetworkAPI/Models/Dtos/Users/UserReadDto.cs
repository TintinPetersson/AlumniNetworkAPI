using AlumniNetworkAPI.Models.Domain;

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

        public virtual ICollection<Topic>? Topics { get; set; }
        public virtual ICollection<Group>? Groups { get; set; }
        public virtual ICollection<Event>? AcceptedEvents { get; set; }
        public virtual ICollection<Event>? UnrespondedEvents { get; set; }
        public virtual ICollection<Post>? AuthoredPosts { get; set; }
        public virtual ICollection<Post>? RecievedPosts { get; set; }
    }
}
