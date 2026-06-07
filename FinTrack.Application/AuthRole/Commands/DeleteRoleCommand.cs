using FinTrack.Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.AuthRole.Commands
{
    public class DeleteRoleCommand: IRequest<ResultModel>
    {
        public string Id { get; set;}
        public DeleteRoleCommand(string id)
        { 
            Id = id;
        }
    }
}
