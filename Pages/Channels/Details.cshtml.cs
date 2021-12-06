using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Final.Data;

namespace Final.Pages.Channels
{
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext context;
        private readonly IDataRepository data;

        //Dependency Injection
        public DetailsModel(ApplicationDbContext context, IDataRepository data)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.data = data;
        }

        [BindProperty]
        public Channel Channel { get; set; }
        public IEnumerable<Topics> TopicsList { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            //get a topic list
            TopicsList= await data.GetTopicListAsync(id);
            //Channel = await data.


            return Page();
        }
    }
}
