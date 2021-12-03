using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Final.Pages.Channels
{
    public class AddChanelModel : PageModel
    {
        private ILogger<AddChanelModel> log;
        private ItemManager itemManager;

        public AddChanelModel(ILogger<AddChanelModel> log, SomeManager itemManager)
        {
            this.log = log;
            this.itemManager = itemManager;

        }
        public void OnGet(string parent, string child)
        {
            ChannelName = parent;
            TopicName = child;
        }

        [BindProperty]
        public string ChannelName { get; set; }
        [BindProperty]
        public string TopicName { get; set; }


    }
}
