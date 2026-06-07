using FinTrack.Application.AuthUser.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.AuthUser.Queries
{
    public class GetUserByIdCommand : IRequest<UserReturnVM>
    {
        public string Id { get; set; }
        public GetUserByIdCommand(string id)
        {
            Id = id;
        }
    }
}
