using FinTrack.Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.Requests.Jamuna.Menus_Url.Commands
{
    public class DeleteMenuUrlCommand : IRequest<ResultModel>
    {
        public long Id { get; set; }
        public DeleteMenuUrlCommand(long id)
        {
            Id = id;
        }
    }
}
