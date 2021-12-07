using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Final.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Final.Pages.Channels
{
    [Authorize]
    public class AddTopicsModel : PageModel
    {

        private readonly ApplicationDbContext dbContext;
        private readonly IDataRepository dataRepository;
        private readonly IAuthorizationService authorizationService;
        private readonly ILogger<AddTopicsModel> logger;

        public AddTopicsModel(ApplicationDbContext dbContext, IDataRepository dataRepository, IAuthorizationService authorizationService,ILogger<AddTopicsModel> logger)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            this.dataRepository = dataRepository;
            this.authorizationService = authorizationService;
            this.logger = logger;
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
            logger.LogInformation("New Topic created by: {adminName} {Topic} ", User.Identity.Name, Topic.TopicTitle);

            //dataRepository.AddTopicAsync(Channel.ChannelId, topic);
            return RedirectToPage("Details", new { slug = slug});
        }

        
    }
}
