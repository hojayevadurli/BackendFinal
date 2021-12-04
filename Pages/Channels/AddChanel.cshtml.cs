using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Channels;
using System.Threading.Tasks;
using Final.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Channel = Final.Data.Channel;

namespace Final.Pages.Channels
{
    //  https://stackoverflow.com/questions/2920744/url-slugify-algorithm-in-c
    // https://stackoverflow.com/questions/249087/how-do-i-remove-diacritics-accents-from-a-string-in-net

    public static class Slug
    {
        public static string GenerateSlug(this string phrase)
        {
            string str = phrase.RemoveDiacritics().ToLower();
            // invalid chars
            str = Regex.Replace(str, @"[^a-z0-9\s-]", "");
            // convert multiple spaces into one space
            str = Regex.Replace(str, @"\s+", " ").Trim();
            // cut and trim
            str = str.Substring(0, str.Length <= 45 ? str.Length : 45).Trim();
            str = Regex.Replace(str, @"\s", "-"); // hyphens
            return str;


        }

        public static string RemoveDiacritics(this string text)
        {
            var s = new string(text.Normalize(NormalizationForm.FormD)
            .Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
            .ToArray());

            return s.Normalize(NormalizationForm.FormC);
        }
    }

    [Authorize]
    public class AddChanelModel : PageModel
    {
        //private ILogger<AddChanelModel> log;
       //private ItemManager itemManager;
        private readonly ApplicationDbContext dbContext;

        public AddChanelModel(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            //this.itemManager = itemManager;

        }
        //public void OnGet(Channel parent, Topics child)
        //{

        //    Channel = parent;
        //    TopicName = child;

        //}

        [BindProperty]
        public Channel Channel { get; set; }
        //[BindProperty]
       // public Topics TopicName { get; set; }

        public async Task<IActionResult> OnPostAddChannel()
        {
            Channel.Slug = Channel.Title.GenerateSlug();
            if (!ModelState.IsValid)
            {
                return Page();
            }
            dbContext.Posts.Add(Channel);
            await dbContext.SaveChangesAsync();
            return RedirectToPage("/Index");
            //log.LogInformation("Adding new Channel: {parent}", ChannelName);
            //itemManager.Channels.Add(new Channel { Title = ChannelName }); 
            //return RedirectToPage();

        }

        //public IActionResult OnPostAddTopic()
        //{
        //    log.LogInformation("Adding :{child} to this channel {parent}", TopicName, ChannelName);
        //    itemManager.Channels.First(i => i.Title == ChannelName).Topic.Add(TopicName);
        //    return RedirectToPage(new { parent = ChannelName });
        //}


    }
}
