using FinTrack.Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.AuthRole.Commands
{
    public class UpdateRoleCommand : IRequest<ResultModel>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ShownAs { get; set; }

        public UpdateRoleCommand(string id, string name, string shownAs)
        {
            Id = id;
            Name = name;
            ShownAs = shownAs;
        }
    }
}
