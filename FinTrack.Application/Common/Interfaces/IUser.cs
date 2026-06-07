using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.Common.Interfaces
{
    public interface IUser
    {
        string Id { get; }
        string Email { get; }
        string UserName { get; }
        string Name { get; }
        string Role { get; }
    }
}
