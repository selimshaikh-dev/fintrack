using FinTrack.Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.AuthView.Commands
{
    public class DeleteMenuCommand : IRequest<Result>
    {
        public long Id { get; set; }
        public DeleteMenuCommand(long id)
        {
                Id = id;
        }
    }
}
