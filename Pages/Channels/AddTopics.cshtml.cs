using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Final.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace Final.Pages.Channels
{
    [Authorize]
    public class AddTopicsModel : PageModel
    {

        private readonly ApplicationDbContext dbContext;
        private readonly IDataRepository dataRepository;
        private readonly IAuthorizationService authorizationService;

        public AddTopicsModel(ApplicationDbContext dbContext, IDataRepository dataRepository, IAuthorizationService authorizationService)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            this.dataRepository = dataRepository;
            this.authorizationService = authorizationService;
        }
       [BindProperty]
        public Channel Channel { get; set; }
        [BindProperty]
        public Topics Topic { get; set; }
               
        public async Task<IActionResult> OnGetAsync(string channelSlug)
        {
           
            Channel = await dataRepository.GetChannelBySlugAsync(channelSlug);
            if (channelSlug == null)
            {
                return NotFound();

                //Channel = JsonConvert.DeserializeObject<Channel>(channelSlug);
            }
            return Page();

        }

        //adding new topics to the channel
        public IActionResult OnPostAddTopics(Topics topic)
        {
            Topic.Slug = Topic.TopicTitle.GenerateSlug();
           // Topic.ChannelsId = channelId;
             
            if (Channel !=null|| ModelState.IsValid)
            {
                //This channel id might crash
                dataRepository.AddTopicAsync(Channel.ChannelId, topic);
            }

            //dataRepository.AddTopicAsync(Channel.ChannelId, topic);
            return RedirectToPage("Details", new { channelSlug = Channel.Slug });
        }

        
    }
}
