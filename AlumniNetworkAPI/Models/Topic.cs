using System.ComponentModel.DataAnnotations;

namespace AlumniNetworkAPI.Models
{
    public class Topic
    {
        public int Id { get; set; }
        [MaxLength(40)]
        [Required]
        public string Name { get; set; }
        [MaxLength(200)]
        public string? Description { get; set; }

        public virtual ICollection<Event>? Events { get; set; }
        public virtual ICollection<User>? Users { get; set; }
        public virtual ICollection<Post>? Posts { get; set; }
    }
}