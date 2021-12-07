using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Final.Data;
using Microsoft.AspNetCore.Authorization;

namespace Final.Pages.Channels
{
    public class TopicDetailsModel : PageModel
    {
        private readonly ApplicationDbContext context;
        private readonly IAuthorizationService authorizationService;
        private readonly IDataRepository data;

        //Dependency Injection
        public TopicDetailsModel(ApplicationDbContext context, IAuthorizationService authorizationService, IDataRepository data)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.authorizationService = authorizationService;
            this.data = data;
        }

        [BindProperty]
        public Topic Topic { get; set; }

        public async Task<IActionResult> OnGetAsync(string topicSlug)
        {
            if (topicSlug == null)
            {
                return NotFound();
            }

            //Topic = await _context.Topics
            //    .Include(t => t.Channel).FirstOrDefaultAsync(m => m.TopicID == id);

            Topic = await data.GetTopicBySlugAsync(topicSlug);

            if (Topic == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
