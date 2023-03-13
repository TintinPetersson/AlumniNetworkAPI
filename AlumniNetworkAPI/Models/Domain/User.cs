using System.ComponentModel.DataAnnotations;

namespace AlumniNetworkAPI.Models.Domain
{
    public class User
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(int.MaxValue)]
        public string KeycloakId { get; set; }
        [MaxLength(40)]
        [Required]
        public string Username { get; set; }
        [MaxLength(int.MaxValue)]
        public string? Picture { get; set; }
        [MaxLength(20)]
        [Required]
        public string Status { get; set; }
        [MaxLength(200)]
        public string? Bio { get; set; }
        [MaxLength(60)]
        public string? FunFact { get; set; }


        //Many-to-many
        public virtual ICollection<Topic>? Topics { get; set; }
        public virtual ICollection<Group>? Groups { get; set; }
        public virtual ICollection<Event>? AcceptedEvents { get; set; }
        public virtual ICollection<Event>? UnrespondedEvents { get; set; }

        //One-to-many
        public virtual ICollection<Post>? AuthoredPosts { get; set; }
        public virtual ICollection<Post>? RecievedPosts { get; set; }
        public ICollection<Event>? AuthoredEvents { get; set; }
    }
}