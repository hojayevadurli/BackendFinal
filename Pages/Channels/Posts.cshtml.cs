using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Final.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

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
        public List<Comment> Comments { get; set; }


        public async Task<IActionResult> OnGetAsync(string slug)
        {


            if (slug == null)
            {
                return NotFound();
            }

            Post = await data.GetPostAsync(slug);

           
            Comments= await context.Comments
                .Include(s=>s.ChildComments)
                .ThenInclude(s=>s.ChildComments)
                .OrderBy(s => s.AddedOn)
                .ToListAsync();

            AddCommentModel = new AddCommentPartialModel();
            AddCommentModel.ParentPostId = Post.Id;

            if (Post == null)
            {
                return NotFound();
            }
            return Page();
        }


        public async Task<IActionResult> OnPostAsync(Comment comment, int ParentSuggestionId)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            comment.AddedOn = DateTime.Now;
            if (ParentSuggestionId > 0)
            {
                comment.ParentId = ParentSuggestionId;
            }

           await data.AddCommentAsync(comment);
            //context.Comments.Add(comment);
            //await context.SaveChangesAsync();

            return RedirectToPage();
        }

        public AddCommentPartialModel AddCommentModel { get; set; } = new();

        

    }
}
