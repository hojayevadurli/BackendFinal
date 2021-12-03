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
        public string TopicName { get; set; }
        public string TopicDescription { get; set; }


        List<Post> Posts { get; set; }
    }
}
