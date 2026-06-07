using FinTrack.Application.AuthView.Commands;
using FinTrack.Application.Common.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.AuthView.Models
{
    public class MenuToUserPrivilegeVM : IMapFrom<CreateMenuToUserPrivilegeCommand>
    {
        public long Id { get; set; }
        public long MenuId { get; set; }
        public string UserId { get; set; }
        public int Type { get; set; }
        public bool Checked { get; set; }
    }
}
