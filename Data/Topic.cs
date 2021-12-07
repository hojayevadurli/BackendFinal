using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Final.Data
{
    public class Topic
    {

        //topics ARE going to have list of posts
        [Key]
        public int TopicID { get; set; }
        public string TopicTitle { get; set; }
        public string Slug { get; set; }

        public string TopicDescription { get; set; }

        public Channel Channel { get; set;}
       public int ChannelId { get; set; }
        public List<Post> Posts { get; set; } = new();
    }
}
