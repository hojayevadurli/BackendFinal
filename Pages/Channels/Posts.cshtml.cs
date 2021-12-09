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
    public class PostsModel : PageModel
    {

        private readonly ApplicationDbContext context;
        private readonly IAuthorizationService authorizationService;
        private readonly IDataRepository data;

        //Dependency Injection
        public PostsModel(ApplicationDbContext context, IAuthorizationService authorizationService, IDataRepository data)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.authorizationService = authorizationService;
            this.data = data;
        }


        public Post Post { get; set; }

        public async Task<IActionResult> OnGetAsync(string slug)
        {


            if (slug == null)
            {
                return NotFound();
            }

            Post = await data.GetPostAsync(slug);
            //Topic = await _context.Topics
            //    .Include(t => t.Channel).FirstOrDefaultAsync(m => m.TopicID == id);


            if (Post == null)
            {
                return NotFound();
            }
            return Page();
        }
        
    }
}
