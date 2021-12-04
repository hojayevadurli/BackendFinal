using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Final.Data
{
    public class Topics
    {

        //topics ARE going to have list of posts
        public int TopicID { get; set; }
        public string TopicTitle { get; set; }
        public string TopicDescription { get; set; }

        public Channel Channel { get; set;}
        public List<Post> Posts { get; set; } = new();
    }
}
