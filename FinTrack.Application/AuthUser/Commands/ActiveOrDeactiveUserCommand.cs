using FinTrack.Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.AuthUser.Commands
{
    public class ActiveOrDeactiveUserCommand : IRequest<ResultModel>
    {
        public string Id { get; set; }
        public bool IsActive { get; set; }
        public ActiveOrDeactiveUserCommand( string id, bool isActive)
        {
            Id = id;
            IsActive = isActive;
        }
    }
}
