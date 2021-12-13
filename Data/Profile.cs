using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Final.Data
{
    public class Profile
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string Bio { get; set; }
        public int Score { get; set; }
        public string Avatar { get; set; }
        public string ProFileFileName { get; set; }
    }
}
