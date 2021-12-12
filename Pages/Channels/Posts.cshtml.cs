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

        
        [BindProperty]
        public Post Post { get; set; }
        [BindProperty]
        public List<Comment> ChildComments { get; set; }
       
        public AddCommentPartialModel AddCommentModel { get; set; } = new();


        public async Task<IActionResult> OnGetAsync(string slug)
        {


            if (slug == null)
            {
                return NotFound();
            }

            Post = await data.GetPostAsync(slug);


            ChildComments = await context.Comments
                .Include(s=>s.ChildComments)
                .ThenInclude(s=>s.ChildComments)
                .ThenInclude(s => s.ChildComments)
                .ThenInclude(s => s.ChildComments)
                .Where(c=>c.PostId ==Post.Id)                
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


        public async Task<IActionResult> OnPostAsync(Comment comment, int? ParentSuggestionId, int? commentID)
        {
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}
            if (((ParentSuggestionId ?? 0) == 0 && (commentID ?? 0)==0 || comment.Text == null))
            {
                return RedirectToPage();
            }
            comment.AddedOn = DateTime.Now;
            if (ParentSuggestionId > 0)
            {
                comment.PostId = ParentSuggestionId;
            }
            else
            {
                comment.CommentId = commentID;
            }

           await data.AddCommentAsync(comment);
            

            return RedirectToPage();
        }

        public DisplayCommentPartialModel CreateDisplayCommentModel(Comment comment)
        {
            var model = new DisplayCommentPartialModel();
            model.Comment=comment ;
            return model;
        }


    }
}
