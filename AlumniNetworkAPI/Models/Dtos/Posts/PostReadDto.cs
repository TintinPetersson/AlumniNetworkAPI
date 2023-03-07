using AlumniNetworkAPI.Models.Domain;
using AlumniNetworkAPI.Models.Dtos.Users;
using System.ComponentModel.DataAnnotations;

namespace AlumniNetworkAPI.Models.Dtos.Posts
{
    public class PostReadDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public DateTime LastUpdated { get; set; }
        public string? Body { get; set; }
        public int? AuthorId { get; set; }
        public UserPostReadDto? Author { get; set; }
        public int? RecieverId { get; set; }
        public int? TopicId { get; set; }
        public int? GroupId { get; set; }
        public int? EventId { get; set; }
        public int? ParentId { get; set; }

        public ICollection<PostGroupReadDTO>? Replies { get; set; } //One-Many
    }
}
