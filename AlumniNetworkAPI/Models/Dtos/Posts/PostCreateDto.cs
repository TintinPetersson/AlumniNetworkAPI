using AlumniNetworkAPI.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace AlumniNetworkAPI.Models.Dtos.Posts
{
    public class PostCreateDto
    {

        public string Body { get; set; }
        public string Title { get; set; }
        public virtual User Author { get; set; }
        public virtual User? Reciever { get; set; }
        public virtual Topic? Topic { get; set; }
        public virtual Group? Group { get; set; }
        public virtual Event? Event { get; set; }
        public virtual Post? ParentPost { get; set; }

    }
}
