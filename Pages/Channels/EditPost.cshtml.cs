﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Final.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace Final.Pages.Channels
{
    public class EditPostModel : PageModel
    {

        private readonly ApplicationDbContext dbContext;
        private readonly IDataRepository dataRepository;
        private readonly IAuthorizationService authorizationService;
        private readonly ILogger<EditPostModel> logger;


        public EditPostModel(ApplicationDbContext dbContext, IDataRepository dataRepository, IAuthorizationService authorizationService, ILogger<EditPostModel> logger)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            this.dataRepository = dataRepository;
            this.authorizationService = authorizationService;
            this.logger = logger;
        }

        [BindProperty]
        public Post Post { get; set; }
        public Topic Topic { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //Post = await dataRepository.GetPostAsync(id);
            Post = await dbContext.Posts
                .Include(p => p.Topic).FirstOrDefaultAsync(m => m.Id == id);
            //Topic = await dbContext.Topics
            //   .Include(p => p.Topic).FirstOrDefaultAsync(m => m.Id == id);
            if (Post == null)
            {
                return NotFound();
            }
           //ViewData["TopicId"] = new SelectList(_context.Topics, "TopicID", "TopicID");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        //public async Task<IActionResult> OnPostAsync()
        //{            
            
        //    Post.Slug = Post.Title.GenerateSlug();
        //    if(!ModelState.IsValid)
        //    {
        //        return Page();
        //    }
        //    // await dataRepository.GetPostAsync(slug);
        //    dbContext.Attach(Post).State = EntityState.Modified;

        //    //if (Post != null)
        //    //{

        //    //    await dataRepository.EditPostAsync(slug, Post);
                
        //    //}

        //    //try 

        //    //logger.LogInformation("Post edited by: {adminName} {Post} ", User.Identity.Name, Post.Title);
        //    //return RedirectToPage("Posts", new { slug = slug });
        //}

        
    }
}
