using System;
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
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IDataRepository dataRepository;
        private readonly IAuthorizationService authorizationService;
        private readonly ILogger<EditModel> logger;

        public EditModel(ApplicationDbContext dbContext, IDataRepository dataRepository, IAuthorizationService authorizationService, ILogger<EditModel> logger)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            this.dataRepository = dataRepository;
            this.authorizationService = authorizationService;
            this.logger = logger;
        }

        [BindProperty]
        public Channel Channel { get; set; }
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
            Channel = await dataRepository.GetChannelBySlugAsync(slug);

            if (Channel == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostEdit(string slug)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            dataRepository.EditChannelAsync(slug);            
          

            return RedirectToPage("./Index");
        }

       
    }
}
