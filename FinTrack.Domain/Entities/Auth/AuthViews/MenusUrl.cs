using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Domain.Entities.Auth.AuthViews
{
    public class MenusUrl
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool Deleted { get; set; }
        public virtual ICollection<Menu> Menus { get; set; }
    }
}
