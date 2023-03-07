using AlumniNetworkAPI.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace AlumniNetworkAPI.Models.Dtos.Posts
{
    public class PostReadDto
    {
        public int Id { get; set; }
        public DateTime LastUpdated { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public int AuthorId { get; set; }
        public User Author { get; set; }

        public int? RecieverId { get; set; }
        public User? Reciever { get; set; }

        public int? TopicId { get; set; }
        public Topic? Topic { get; set; }

        public int? GroupId { get; set; }
        public Group? Group { get; set; }

        public int? EventId { get; set; }
        public Event? Event { get; set; }

        public int ParentPostId { get; set; }
        public Post? ParentPost { get; set; }

        public ICollection<Post>? Replies { get; set; }
    }
}
