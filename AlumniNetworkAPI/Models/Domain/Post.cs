﻿using System.ComponentModel.DataAnnotations;

namespace AlumniNetworkAPI.Models.Domain
{
    public class Post
    {
        public int Id { get; set; }
        [Required]
        public DateTime LastUpdated { get; set; }
        [MaxLength(255)]
        public string? Title { get; set; }
        public string Body { get; set; }


        public virtual int AuthorId { get; set; }
        public virtual User Author { get; set; }

        public virtual int? RecieverId { get; set; }
        public virtual User? Reciever { get; set; }

        public virtual int? TopicId { get; set; }
        public virtual Topic? Topic { get; set; }

        public virtual int? GroupId { get; set; }
        public virtual Group? Group { get; set; }

        public virtual int? EventId { get; set; }
        public virtual Event? Event { get; set; }

        public virtual int? ParentId { get; set; }
        public virtual Post? Parent { get; set; }

        public virtual ICollection<Post>? Replies { get; set; }
    }
}