using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.AuthView.Models
{
    public class MenuItemReturnVM
    {
        public long Id { get; set; }
        public long? ParentId { get; set; }
        public string Title { get; set; }
        public string URL { get; set; }
        public int DisplayOrder { get; set; }
        public string IconClass { get; set; }
        public bool Has2ndLevelChild { get; set; }
        public bool Has3rdLevelChild { get; set; }
        public List<MenuItemReturnVM> SubMenuList { get; set; } = new List<MenuItemReturnVM>();
    }
}
