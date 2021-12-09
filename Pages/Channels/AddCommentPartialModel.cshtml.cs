using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Final.Data;

namespace Final.Pages.Channels
{
    public class AddCommentPartialModelModel : PageModel
    {
        public Comment Comment { get; set; }
        
        public int ParentCommentID { get; set; }
        public AddCommentPartialModelModel(int parentComment)
        {
            ParentCommentID = parentComment;
        }
        public void OnGet()
        {
        }
    }
}
