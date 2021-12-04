using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Final.Data
{
    [Keyless]
    public class Channel
    {
        //[Key]
        public int ChannelId { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }

        public List<Topics> Topic { get; set; } = new();

    }

    //public class ItemManager
    //{
    //    public List<Channel> Channels { get; set; } = new();
    //}
}
