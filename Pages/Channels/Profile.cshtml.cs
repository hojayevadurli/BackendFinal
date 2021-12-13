using System;
using System.Collections.Generic;
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
        public const string PictureFolder = "Pages/ProfileImages";

        public ProfileModel(ApplicationDbContext dbContext, IDataRepository dataRepository, IAuthorizationService authorizationService)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            this.dataRepository = dataRepository;
            this.authorizationService = authorizationService;
        }
        public void OnGet()
        {
            Name = User.Identity.Name;

        }

        public string Name { get; set; }
        public string PathToAvatar { get; set; }
        public IFormFile File { get; set; }
    }
}
