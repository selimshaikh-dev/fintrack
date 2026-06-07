using FinTrack.Application.Common.Interfaces;
using FinTrack.Application.Common.Models;
using FinTrack.Infrastructure.Data;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Infrastructure.Services.Jamuna
{
    public class ServerDateTimeService : SqlDbContext<DateTimeVM>, IServerDateTimeService
    {
        private readonly ApplicationDbContext _context;

        public ServerDateTimeService(IConfiguration configuration, ApplicationDbContext context) : base(configuration)
        {
            _context = context;
        }
        public async Task<DateTime> GetServerDateTimeAsync()
        {
            string query = "SELECT GETDATE() as ServerDateTime";
            var data = await GetServerDateTimeAsync(query, null);
            return data;
        }
    }
}
