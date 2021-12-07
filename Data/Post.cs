using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Final.Data
{
    public class Post
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Body { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public DateTime Published { get; set; }
        public DateTime? LastEditedOn { get; internal set; } = DateTime.Now;
        public string LastEditedBy { get; set; }
        public Topic Topic { get; set; }
        public int TopicId { get; set; }
        public List<Comment> Comments { get; set; } = new();
      
    }
}
