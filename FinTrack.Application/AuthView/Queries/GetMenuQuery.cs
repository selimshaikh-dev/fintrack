using FinTrack.Application.AuthView.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.AuthView.Queries
{
    public class GetMenuQuery : IRequest<IList<MenuItemVM>>
    {
        public string Name { get; set; }
        //get upto 5 level menu
        public int MenuLevel { get { return 5; } }
        public GetMenuQuery(string name)
        {
            Name = name;
        }
    }
}
