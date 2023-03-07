using AlumniNetworkAPI.Models.Dtos.Users;

namespace AlumniNetworkAPI.Models.Dtos.Posts
{
    public class PostGroupReadDTO
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public DateTime LastUpdated { get; set; }
        public string? Body { get; set; }
        public int? AuthorId { get; set; }
        public UserPostReadDto? Author { get; set; }
        public int? ParentId { get; set; }

    }
}
