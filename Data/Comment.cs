using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Final.Data
{
    public class Comment
    {

        //is going to have list of comments and then 
        [Key]
        public int Id { get; set; }
        
        public string Text { get; set; }

        public string Author { get; set; }
        public DateTime AddedOn { get; set; }
        public Post Post { get; set; }
        public int ParentId { get; set; }

        public List<Comment> ChildComments { get; set; }


    }
}
