using FinTrack.Application.Requests.Jamuna.Client_Jamuna.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.Requests.Jamuna.Client_Jamuna.Queries
{
    public class GetClientInfosByEmailQuery : IRequest<ClientInfosJamunaVM>
    {
        public string Email { get; set; }
        public GetClientInfosByEmailQuery(string email)
        {
            Email = email; 
        }
    }
}
