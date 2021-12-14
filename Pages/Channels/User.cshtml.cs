using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Final.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Final.Pages.Channels
{
    [Authorize]
    public class UserModel : PageModel
    {

        private readonly ApplicationDbContext dbContext;
        private readonly IDataRepository dataRepository;
        private readonly IAuthorizationService authorizationService;
        private readonly ILogger<UserModel> logger;
        private readonly IUserService userService;
         string Path1 = "/ProfileImages/Images";
        public UserModel(ApplicationDbContext dbContext, IDataRepository dataRepository, IAuthorizationService authorizationService, IUserService userService)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            this.dataRepository = dataRepository;
            this.authorizationService = authorizationService;
            this.userService = userService;
        }
        public IList<Post> userPosts { get; set; }
        public IList<Comment> userComments { get; set; }
        // public string Name { get; set; }
        public string PathToAvatar { get; set; }
        [BindProperty]
        public IFormFile File { get; set; }

        public Profile newUser { get; set; }
        public string currUser { get; set; }


        public async Task OnGet(string UserName)
        {
            currUser = UserName;
            userComments = await dbContext.Comments.Where(c => c.Author == currUser).ToListAsync();
            userPosts = await dbContext.Posts.ToListAsync();
            newUser = await userService.GetProfileAsync(currUser);

            //dbContext.SaveChanges();
           
            if(newUser?.ProFileFileName!=null)
            {
                //  

                PathToAvatar = Path.Combine(Path1, newUser.ProFileFileName);
            }

        }
        //Name = User.Identity.Name; Path newUser.ProFileFileName;

        public async Task<IActionResult> OnPostAsync(string UserName)
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }

            await userService.SaveAvatarAsync(File, UserName);

            //var infoUser = dbContext.Users.FirstOrDefault(p => p.Name == User.Identity.Name); 
            //if(infoUser==null)
            //{
            //    infoUser = new Profile() { Name = User.Identity.Name };
            //    dbContext.Users.Add(infoUser);
            //}

            

            return RedirectToPage();
        }
    }

    public interface IUserService
    {
        Task<Profile> GetProfileAsync(string UserName);
        Task SaveAvatarAsync(IFormFile formFile, string UserName);
    }

}
