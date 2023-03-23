namespace AlumniNetworkAPI.Models.Dtos.Posts
{
    public class PostReplyCreateDto
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public int? RecieverId { get; set; }
        public int? ParentId { get; set; }
    }
}
