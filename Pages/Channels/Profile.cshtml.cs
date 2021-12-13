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
using Microsoft.Extensions.Logging;

namespace Final.Pages.Channels
{
    [Authorize]
    public class ProfileModel : PageModel
    {

        private readonly ApplicationDbContext dbContext;
        private readonly IDataRepository dataRepository;
        private readonly IAuthorizationService authorizationService;
        private readonly ILogger<ProfileModel> logger;
        public const string PictureFolder = "wwwroot/images/ProfileImages";

        public ProfileModel(ApplicationDbContext dbContext, IDataRepository dataRepository, IAuthorizationService authorizationService)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            this.dataRepository = dataRepository;
            this.authorizationService = authorizationService;
        }
        public async Task OnGet(string slug)
        {
            Name = User.Identity.Name;
            var profileInfo = dbContext.Users.FirstOrDefault(p => p.Name == slug);
            if(profileInfo!=null)
            {
                PathToAvatar = Path.Combine(PictureFolder);
            }

        }

        public string Name { get; set; }
        public string PathToAvatar { get; set; }
        public IFormFile File { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }

            var infoUser = dbContext.Users.FirstOrDefault(p => p.Name == User.Identity.Name); 
            if(infoUser==null)
            {
                infoUser = new Profile() { Name = User.Identity.Name };
                dbContext.Users.Add(infoUser);
            }

            var profileName = User.Identity.Name + Path.GetExtension(File.FileName);
            if(!Directory.Exists(PictureFolder))
            {
                Directory.CreateDirectory(PictureFolder);
            }

            string PathToProfile = Path.Combine("wwwroot", PictureFolder, profileName);
            using var stream = System.IO.File.OpenWrite(PathToProfile);
            await File.CopyToAsync(stream);
            stream.Close();
            infoUser.ProFileFileName= profileName;
            await dbContext.SaveChangesAsync();

            return RedirectToPage();
        }
    }
}
