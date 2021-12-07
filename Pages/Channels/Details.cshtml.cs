using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Final.Data;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;

namespace Final.Pages.Channels
{
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext context;
        private readonly IAuthorizationService authorizationService;
        private readonly IDataRepository data;

        //Dependency Injection
        public DetailsModel(ApplicationDbContext context, IAuthorizationService authorizationService, IDataRepository data)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.authorizationService = authorizationService;
            this.data = data;
        }



        [BindProperty]
        public Channel Channel { get; set; }
      // [BindProperty]
       // public IEnumerable<Topics> TopicsList { get; set; }
        public bool IsAdmin { get; set; }
        public async Task<IActionResult> OnGetAsync(string channelSlug)
        {
            var authReasult= await authorizationService.AuthorizeAsync(User, AuthorizationPolicies.IsAdmin);

            IsAdmin = authReasult.Succeeded;
            if (channelSlug == null)
            {
                return NotFound();

                //Channel = JsonConvert.DeserializeObject<Channel>(channelSlug);
            }
            Channel = await data.GetChannelBySlugAsync(channelSlug);


            if (Channel == null)
            {
                return NotFound();
            }

            
            //TopicsList = Channel.TopicList;
            //TopicsList= await data.GetTopicByChannelSlugAsync(channelSlug);


            return Page();
        }
    }
}
