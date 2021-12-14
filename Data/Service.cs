using Final.Pages.Channels;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Final.Data
{
    public class Service : IUserService
    {
        private readonly ApplicationDbContext dbContext;

        public Service( ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public const string PictureFolder = "./ProfileImages/Images";

        public async Task<Profile> GetProfileAsync(string UserName)
        {
            var profileInfo = await dbContext.Users.FirstOrDefaultAsync(p => p.Name == UserName);
            if (profileInfo==null)
            {
                profileInfo = new Profile { Name = UserName };
                dbContext.Users.Add(profileInfo);
            }

            return profileInfo;
            
        }

        public async Task SaveAvatarAsync(IFormFile formFile, string UserName)
        {
            var profileInfo = await GetProfileAsync(UserName);
            var profileName = UserName + Path.GetExtension(formFile.FileName);
            if (!Directory.Exists(PictureFolder))
            {
                Directory.CreateDirectory(PictureFolder);
            }

            string PathToProfile = Path.Combine("wwwroot", PictureFolder, profileName);
            using var stream = System.IO.File.OpenWrite(PathToProfile);
            await formFile.CopyToAsync(stream);
            stream.Close();
            profileInfo.ProFileFileName = profileName;
            await dbContext.SaveChangesAsync();
        }
    }
}
