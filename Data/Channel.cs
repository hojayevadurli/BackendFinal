using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Final.Data
{
   
    public class Channel
    {
        [Key]
        public int ChannelId { get; set; }
        [Required]
        public string Title { get; set; }
        public string Slug { get; set; }
        [Required]
        public string Description { get; set; }

        public List<Topic> TopicList { get; set; } = new();

    }

   
}
