using FinTrack.Application.AuthView.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.AuthView.Queries
{
    public class GetMenuByIdQuery : IRequest<MenuItemVM>
    {
        public long Id { get; set; }    
        public GetMenuByIdQuery(long id) 
        {
            Id = id;
        }
    }
}
