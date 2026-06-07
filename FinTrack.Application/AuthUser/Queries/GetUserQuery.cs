using FinTrack.Application.AuthUser.ViewModels;
using FinTrack.Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.AuthUser.Queries
{
    public class GetUserQuery : IRequest<IEnumerable<UserReturnVM>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public GetUserQuery(int pn, int ps)
        {
            PageNumber = pn;
            PageSize = ps;
        }
    }
}
