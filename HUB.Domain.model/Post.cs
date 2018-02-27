using System;
using System.ComponentModel.DataAnnotations;

namespace HUB.Domain.Model
{
    public class Post
    {
        [Key]
        public Guid PostId { get; set; }
        [MaxLength(50)]
        public string Title { get; set; }
        public string Content { get; set; }
        public Guid AuthorId { get; set; }
        public virtual User Author { get; set; }
    }
}
