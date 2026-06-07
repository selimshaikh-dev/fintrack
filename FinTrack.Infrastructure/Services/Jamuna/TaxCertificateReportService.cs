using Dapper;
using FinTrack.Application.Common.Constants;
using FinTrack.Application.Common.Constants;
using FinTrack.Application.Common.Helpers;
using FinTrack.Application.Common.Interfaces;
using FinTrack.Application.Requests.Jamuna.Client_Jamuna.Interfaces;
using FinTrack.Application.Requests.Jamuna.TaxReport.Interfaces;
using FinTrack.Application.Requests.Jamuna.TaxReport.ViewModels;
using FinTrack.Domain.Enums;
using FinTrack.Infrastructure.Data;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace FinTrack.Infrastructure.Services.Jamuna
{
    public class TaxCertificateReportService : SqlDbContext<CashFlowJamunaVM>, ITaxCertificateReportService
    {
        private readonly ApplicationDbContext _context;
        private readonly IClientServiceJamuna _clientService;
        private readonly IServerDateTimeService _serverDateTimeService;
        private readonly IUser _user;
        public TaxCertificateReportService(IConfiguration configuration, ApplicationDbContext context, IClientServiceJamuna clientService, IServerDateTimeService serverDateTimeService, IUser user) : base(configuration)
        {
            _context = context;
            _clientService = clientService;
            _serverDateTimeService = serverDateTimeService;
            _user = user;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        [Obsolete]
        public async Task<TaxCertificateReportJamunaVM> GetTaxCertificateReport(string memberID, string financialYear)
        {
            TaxCertificateReportJamunaVM objTaxCertificateReport = new TaxCertificateReportJamunaVM();
            objTaxCertificateReport.ClientDetails = new ClientDetailsTaxVM();
            try
            {
                DateTime todayDate = await _serverDateTimeService.GetServerDateTimeAsync();
                DateTime StartDate = new DateTime();
                DateTime EndDate = new DateTime();
                var id = _user.Id;
                var role = _user.Role;

                memberID = memberID.PadLeft(6, '0').PadLeft(8, '7');
                var clientInfosVM = await _clientService.GetClientInfosJamuna(memberID);
                if (clientInfosVM == null)
                {
                    objTaxCertificateReport.Message = $"Member Code {memberID} is not Jamuna Member!";
                    return objTaxCertificateReport;
                }
                if (!string.IsNullOrEmpty(financialYear))
                {
                    string[] fyear = financialYear.Split('-');
                    if (!string.IsNullOrEmpty(fyear[0]) && !string.IsNullOrEmpty(fyear[1]))
                    {
                        StartDate = new DateTime(Convert.ToInt32(fyear[0]), 07, 01);
                        EndDate = new DateTime(Convert.ToInt32(fyear[1]), 06, 30);
                    }
                    else
                    {
                        objTaxCertificateReport.Message = "Fiscal Year Selection Is Not In Correct Format";
                        return objTaxCertificateReport;
                    }
                }
                objTaxCertificateReport.ClientDetails.MemberId = clientInfosVM.JamunaMemberCode;
                objTaxCertificateReport.ClientDetails.Name = clientInfosVM.BP_Name;
                objTaxCertificateReport.ClientDetails.Father_Name = clientInfosVM.Father_Name;
                objTaxCertificateReport.ClientDetails.Mother_Name = clientInfosVM.Mother_Name;
                objTaxCertificateReport.ClientDetails.TIN = "";
                objTaxCertificateReport.ClientDetails.Email = clientInfosVM.Email ?? "";
                objTaxCertificateReport.ClientDetails.Phone = clientInfosVM.PhoneNumber ?? "";
                objTaxCertificateReport.ClientDetails.NID = "";                
                objTaxCertificateReport.ClientDetails.SubjectLine = $"This is to certify that {clientInfosVM.BP_Name} [Member Id: {clientInfosVM.JamunaMemberCode}]  has been maintaining a account with our organization. Activity summary of this account for the fiscal period from {StartDate.ToString("dd-MMM-yyyy", System.Globalization.CultureInfo.InvariantCulture)} to {EndDate.ToString("dd-MMM-yyyy", System.Globalization.CultureInfo.InvariantCulture)} is given below.";
                objTaxCertificateReport.ClientDetails.Address = string.IsNullOrEmpty(clientInfosVM.Address_Line_1) ? string.IsNullOrEmpty(clientInfosVM.Address_Line_2) ? "" : clientInfosVM.Address_Line_2 : string.IsNullOrEmpty(clientInfosVM.Address_Line_2) ? clientInfosVM.Address_Line_1 : $"{clientInfosVM.Address_Line_1}, {clientInfosVM.Address_Line_2}";
                objTaxCertificateReport.Message = "";
                objTaxCertificateReport.PrintDate = todayDate.ToString("dd-MMMM-yyyy h:mm tt", System.Globalization.CultureInfo.InvariantCulture);

                var Params = string.Format("{0}/{1}/{2}/{3}", clientInfosVM.JamunaMemberCode.TrimStart('7').TrimStart('0'), financialYear, (int)ReportType.TaxCertificate, objTaxCertificateReport.PrintDate);
                string encryptedParams = CryptoHelpers.EncryptStringAES(Params, AppConstant.CryptoSecret);
                objTaxCertificateReport.Params = encryptedParams;

                string query = "Get_Cash_Flow_By_DateRange";
                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("@BP_ID", clientInfosVM.BP_ID_Jamuna, DbType.Int32, ParameterDirection.Input);
                parameter.Add("@Start_Date", StartDate.Date, DbType.Date, ParameterDirection.Input);
                parameter.Add("@End_Date", EndDate.Date, DbType.Date, ParameterDirection.Input);
                objTaxCertificateReport.CashFlow = await GetSingleBySPAsync(query, parameter);
                objTaxCertificateReport.CashFlow.AsOn = EndDate.ToString("dd-MMM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
                return objTaxCertificateReport;
            }
            catch (Exception ex)
            {
                objTaxCertificateReport.Message = "Something went worng due to " + ex.Message;
                return objTaxCertificateReport;
            }
        }
    }
}
