using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Final.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

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
        public Topics Topics { get; set; }
               
        public void OnGet()
        {

        }

        //adding new topics to the channel
        public IActionResult OnPostAddTopics(int channelID,Topics topics )
        {

            if (ModelState.IsValid)
            {
                dataRepository.AddTopicAsync(channelID, topics);               
               
            }
            return Page();
        }

        //public IActionResult OnPostAddChild()
        //{
        //    log.LogInformation("Adding {child} as a child to {parent}", ChildName, ParentName);
        //    itemManager.TopLevelItems.First(i => i.Name == ParentName).Children.Add(ChildName);
        //    return RedirectToPage(new { parent = ParentName });
        //}

    }
}
