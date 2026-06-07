using FinTrack.Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.AuthView.Commands
{
    public class CreateMenuToUserPrivilegeCommand : IRequest<Result>
    {
        public long Id { get; set; }
        public long MenuId { get; set; }
        public string UserId { get; set; }
        public int Type { get; set; }
        public bool Checked { get; set; }
        public CreateMenuToUserPrivilegeCommand(long id, long menuId, string userId, int type, bool ischecked)
        {
            Id = id;
            MenuId = menuId;
            UserId = userId;
            Type = type;
            Checked = ischecked;
        }
    }
}
