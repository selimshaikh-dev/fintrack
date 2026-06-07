using FinTrack.Application.AuthView.Commands;
using FinTrack.Application.Common.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.AuthView.Models
{
    public class MenuToGroupPrivilegeVM : IMapFrom<CreateMenuToGroupPrivilegeCommand>
    {
        public long Id { get; set; }
        public long MenuId { get; set; }
        public string RoleId { get; set; }
    }
}
