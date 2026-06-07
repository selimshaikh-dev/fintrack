
using FinTrack.Application.AuthRole.Commands;
using FinTrack.Application.Common.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.AuthRole.ViewModels
{
    public class RoleVM:IMapFrom<CreateRoleCommand>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ShownAs { get; set; }
    }
    public class RoleUpdateVM : IMapFrom<UpdateRoleCommand>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ShownAs { get; set; }
    }
}
