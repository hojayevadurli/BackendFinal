using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Final.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Final.Pages.Channels
{
    public class AddCommentPartialModel : PageModel
    {

        public Comment Comment { get; set; }

        public int? ParentCommentID { get; set; }
        public int ParentPostId { get; set; }
      
       
        public void OnGet()
        {
        }
    }
}
