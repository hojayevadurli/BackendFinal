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
    public class DeletePostModel : PageModel
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IDataRepository dataRepository;
        private readonly IAuthorizationService authorizationService;
        private readonly ILogger<DeletePostModel> logger;

        public DeletePostModel(ApplicationDbContext dbContext, IDataRepository dataRepository, IAuthorizationService authorizationService, ILogger<DeletePostModel> logger)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            this.dataRepository = dataRepository;
            this.authorizationService = authorizationService;
            this.logger = logger;
        }

        [BindProperty]
        public Post Post { get; set; }
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
            Post = await dataRepository.GetPostAsync(slug);
           
            if (Post == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostDeletePost(string slug)
        {
            if (slug == null)
            {
                return NotFound();
            }

            //make find function in datarepository

            //await dataRepository.GetPostAsync(slug);
            // Channel = await _context.Channels.FindAsync(id);

            if (Post != null)
            {

                await dataRepository.RemovePostAsync(slug);
                //_context.Channels.Remove(Channel);
                //await _context.SaveChangesAsync();
            }

            logger.LogInformation("Post deleted by: {adminName} {Post} ", User.Identity.Name, Post.Title);
            return RedirectToPage("TopicDetails", new { slug = slug });
        }
    }
}
