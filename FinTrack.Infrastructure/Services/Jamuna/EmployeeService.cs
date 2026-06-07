using Dapper;
using FinTrack.Application.Requests.Jamuna.Employee.Interfaces;
using FinTrack.Application.Requests.Jamuna.Employee.ViewModels;
using FinTrack.Infrastructure.Data;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Infrastructure.Services.Jamuna
{
    public class EmployeeService : SqlDbContext<EmployeeVM>, IEmployeeService
    {
        private readonly ApplicationDbContext _context;
        public EmployeeService(IConfiguration configuration, ApplicationDbContext context) : base(configuration)
        {
            _context = context;
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<EmployeeVM> GetEmployeeByEmailAsync(string email)
        {
            string query = "Employee_Infos_By_Email";

            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@email", email, DbType.String, ParameterDirection.Input);
            var data = await GetSingleBySPAsync(query, parameter);
            return data;
        }
        public async Task<EmployeeVM> GetEmployeeByBpIdAsync(int bpId)
        {
            string query = "Employee_Infos_By_BPID";

            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@bpid", bpId, DbType.Int32, ParameterDirection.Input);
            var data = await GetSingleBySPAsync(query, parameter);
            return data;
        }
    }
}
