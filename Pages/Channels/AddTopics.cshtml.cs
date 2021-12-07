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
       
        public Channel Channel { get; set; }
        [BindProperty]
        public Topic Topic { get; set; }
               
        public async Task<IActionResult> OnGetAsync(string slug)
        {
           
            Channel = await dataRepository.GetChannelBySlugAsync(slug);
            if (slug == null)
            {
                return NotFound();

                //Channel = JsonConvert.DeserializeObject<Channel>(channelSlug);
            }
            return Page();

        }

        //adding new topics to the channel
        public async Task<IActionResult> OnPostAddTopics(int channelId, string slug)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Topic.Slug = Topic.TopicTitle.GenerateSlug();
            Topic.ChannelId = channelId;
             
            await dataRepository.AddTopicAsync(channelId, Topic);

            //dataRepository.AddTopicAsync(Channel.ChannelId, topic);
            return RedirectToPage("Details", new { channelSlug = slug});
        }

        
    }
}
