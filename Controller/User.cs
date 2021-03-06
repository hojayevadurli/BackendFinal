using Final.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Final.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class User : ControllerBase
    {
        private readonly ApplicationDbContext context;
     

        public User(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet("{id}", Name ="Get")]
        public async Task<Profile> GetAsync(int id)
        {
            return await context.Users.FirstOrDefaultAsync(m => m.Id == id);
            
        }
    }
}
