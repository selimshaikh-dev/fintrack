using FinTrack.Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.AuthView.Commands
{
    public class CreateMenuToGroupPrivilegeCommand : IRequest<Result>
    {
        public long Id { get; set; }
        public long MenuId { get; set; }
        public string RoleId { get; set; }
        public int Type { get; set; }
        public bool Checked { get; set; }
        public CreateMenuToGroupPrivilegeCommand(long id, long menuId, string roleId, int type, bool ischecked)
        {
            Id = id;
            MenuId = menuId;
            RoleId = roleId;
            Type = type;
            Checked = ischecked;
        }
    }
}
