using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Final.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace Final.Pages.Channels
{
    [Authorize]
    public class AddPostModel : PageModel
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IDataRepository dataRepository;
        private readonly IAuthorizationService authorizationService;
        private readonly ILogger<AddPostModel> logger;

        public AddPostModel(ApplicationDbContext dbContext, IDataRepository dataRepository, IAuthorizationService authorizationService, ILogger<AddPostModel> logger)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            this.dataRepository = dataRepository;
            this.authorizationService = authorizationService;
            this.logger = logger;
        }

        [BindProperty]
        public Topic Topic { get; set; }

        [BindProperty]
        public Post Post { get; set; }

        public async Task<IActionResult> OnGetAsync(string slug)
        {
            Topic = await dataRepository.GetTopicBySlugAsync(slug);

            if (slug == null)
            {
                return NotFound();
            }

            //Topic = await _context.Topics
            //    .Include(t => t.Channel).FirstOrDefaultAsync(m => m.TopicID == id);


            return Page();

        }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAddPostsAsync(int topicId, string slug)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Post.Slug = Post.Title.GenerateSlug();
            Post.TopicId = topicId;

            await dataRepository.AddPostAsync(topicId, Post);
            logger.LogInformation("New Post created by: {adminName} {Post} ", User.Identity.Name, Post.Title);


            return RedirectToPage("TopicDetails", new { slug = slug });
        }
    }
}
