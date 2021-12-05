using Final.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Final.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IAuthorizationService authorizationService;
        private readonly IDataRepository data;

        public IndexModel(ApplicationDbContext _dbContext, IAuthorizationService _authorizationService, IDataRepository data)
        {
            dbContext = _dbContext ?? throw new ArgumentNullException(nameof(_dbContext));
            this.authorizationService = _authorizationService;
            this.data = data;
        }

        public IEnumerable<Channel> ChannelList { get; set; }
        //public IList<Topics> TopicList { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {            

            ChannelList = await data.GetChannelListAsync();

            if (ChannelList == null)
            {
                return NotFound();
            }
            return Page();
        }

        public bool IsAdmin { get; set; }


    }
}
