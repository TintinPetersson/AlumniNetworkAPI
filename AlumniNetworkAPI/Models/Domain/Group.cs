using System.ComponentModel.DataAnnotations;

namespace AlumniNetworkAPI.Models.Domain
{
    public class Group
    {
        public int Id { get; set; }
        [MaxLength(40)]
        [Required]
        public string Name { get; set; } = string.Empty;
        [MaxLength(200)]
        public string? Description { get; set; }
        [Required]
        public bool IsPrivate { get; set; }

        public virtual ICollection<Event>? Events { get; set; } //Many-to-many
        public virtual ICollection<Post>? Posts { get; set; } //One-to-many
        public virtual ICollection<User>? Users { get; set; } //Many-to-many
    }
}