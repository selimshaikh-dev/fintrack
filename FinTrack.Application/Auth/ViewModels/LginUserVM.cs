using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.Auth.ViewModels
{
    public class LginUserVM
    {
        public string Id { get; set; } 
        public string UserName { get; set; } 
        public string Email { get; set; }
        public string Name { get; set; }
        public string UserGroup { get; set; }
        public string ImageUrl { get; set; } = "https://localhost:5001/Images/wanna1.png";

    }
}
