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
    public class AddChannelModel : PageModel
    {
       
        private readonly ApplicationDbContext dbContext;
        private readonly IDataRepository dataRepository;
        private readonly IAuthorizationService authorizationService;
        private readonly ILogger<AddChannelModel>logger;

        public AddChannelModel(ApplicationDbContext dbContext, IDataRepository dataRepository,IAuthorizationService authorizationService, ILogger<AddChannelModel> logger)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            this.dataRepository = dataRepository;
            this.authorizationService = authorizationService;
            this.logger = logger;
            
        }
       
        [BindProperty]
        public Channel Channel { get; set; }
       
        public async Task<IActionResult> OnPostAddChannel()
        {
            Channel.Slug = Channel.Title.GenerateSlug();


            if (!ModelState.IsValid)
            {
                return Page();
            }
            await dataRepository.AddChannelAsync(Channel);
            logger.LogInformation("New Channel created by: {adminName} {Channel} ", User.Identity.Name, Channel.Title);
             return RedirectToPage("/Index");
        }
              


    }
}
