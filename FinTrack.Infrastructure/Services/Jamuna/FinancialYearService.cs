using FinTrack.Application.Common.Interfaces;
using FinTrack.Application.Requests.Jamuna.Financial_Year.Interfaces;
using FinTrack.Application.Requests.Jamuna.Financial_Year.ViewModels;
using FinTrack.Infrastructure.Data;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Infrastructure.Services.Jamuna
{
    public class FinancialYearService : IFinancialYearService
    {
        private readonly IServerDateTimeService _serverDateTimeService;
        public FinancialYearService(IServerDateTimeService serverDateTimeService)
        {
            _serverDateTimeService = serverDateTimeService;
        }
        public async Task<List<FinancialDropdownVM>> GetFinancialYear()
        {
            List<FinancialDropdownVM> lstFinancialYeare = new List<FinancialDropdownVM>();
            var todayDate = await _serverDateTimeService.GetServerDateTimeAsync();
            DateTime financialYearEndDate = new DateTime(todayDate.Year, 06, 30);

            int financialStartYear;
            int financialEndYear;

            if (todayDate.Date < financialYearEndDate.Date)
            {
                financialStartYear = todayDate.AddYears(-1).Year;
                financialEndYear = todayDate.Year;
            }
            else
            {
                financialStartYear = todayDate.Year;
                financialEndYear = todayDate.AddYears(1).Year;
            }

            for (int i = 0; i <= 5; i++)
            {
                FinancialDropdownVM objFinancialDropdown = new FinancialDropdownVM();

                var financialYear = $"{financialStartYear - i}-{financialEndYear - i}";
                objFinancialDropdown.Text = financialYear;
                objFinancialDropdown.Value = financialYear;
                if (financialStartYear - i >= 2022)
                {
                    lstFinancialYeare.Add(objFinancialDropdown);
                }
            }
            return lstFinancialYeare;
        }
    }
}
