using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Final.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace Final.Pages.Channels
{
    [Authorize]
    public class DeleteChannelModel : PageModel
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IDataRepository dataRepository;
        private readonly IAuthorizationService authorizationService;
        private readonly ILogger<DeleteChannelModel> logger;

        public DeleteChannelModel(ApplicationDbContext dbContext, IDataRepository dataRepository, IAuthorizationService authorizationService, ILogger<DeleteChannelModel> logger)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            this.dataRepository = dataRepository;
            this.authorizationService = authorizationService;
            this.logger = logger;
        }

        [BindProperty]
        public Channel Channel { get; set; }
        public bool IsAdmin { get; set; }

        public async Task<IActionResult> OnGetAsync(string slug)
        {
            var authReasult = await authorizationService.AuthorizeAsync(User, AuthorizationPolicies.IsAdmin);

            IsAdmin = authReasult.Succeeded;
            if (slug == null)
            {
                return NotFound();

                //Channel = JsonConvert.DeserializeObject<Channel>(channelSlug);
            }
            Channel = await dataRepository.GetChannelBySlugAsync(slug);

            if (Channel == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string slug)
        {
            if (slug == null)
            {
                return NotFound();
            }

            //make find function in datarepository

            await dataRepository.GetChannelBySlugAsync(slug);
           // Channel = await _context.Channels.FindAsync(id);

            if (Channel != null)
            {

               await dataRepository.RemoveChannelAsync(slug, Channel);
                //_context.Channels.Remove(Channel);
                //await _context.SaveChangesAsync();
            }

            return RedirectToPage("/Index");
        }
    }
}
