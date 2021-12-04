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



        public IndexModel(ApplicationDbContext _dbContext, IAuthorizationService _authorizationService)
        {
            dbContext = _dbContext ?? throw new ArgumentNullException(nameof(_dbContext));
            this.authorizationService = _authorizationService;
        }

        public List<Channel> ChannelList { get; set; } = new();
        //public IList<Topics> TopicList { get; set; }


         public bool IsAdmin { get; set; }


    }
}
