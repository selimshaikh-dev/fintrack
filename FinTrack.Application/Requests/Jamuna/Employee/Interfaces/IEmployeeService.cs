using FinTrack.Application.Requests.Jamuna.Employee.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.Requests.Jamuna.Employee.Interfaces
{
    public interface IEmployeeService : IDisposable
    {
        Task<EmployeeVM> GetEmployeeByEmailAsync(string email);
        Task<EmployeeVM> GetEmployeeByBpIdAsync(int bpId);
    }
}
