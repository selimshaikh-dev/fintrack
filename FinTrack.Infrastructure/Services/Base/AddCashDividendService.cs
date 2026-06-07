using Dapper;
using FinTrack.Application.AuthView.Models;
using FinTrack.Application.Commands.Base.Interfaces;
using FinTrack.Application.Common.Interfaces;
using FinTrack.Application.Common.Models;
using FinTrack.Application.Requests.Base.Client_Dividend.Interfaces;
using FinTrack.Application.Requests.Base.Client_Dividend.ViewModels;
using FinTrack.Domain.Entities.Auth.AuthViews;
using FinTrack.Infrastructure.Data;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FinTrack.Infrastructure.Services.Base
{
    public class AddCashDividendService : SqlDbContextBase<CashDividendVM>, IAddCashDividendService
    {
        private readonly ApplicationDbContext _context;
        private readonly IInstrumentService _instrumentService;

        public AddCashDividendService(
               IConfiguration configuration,
               ApplicationDbContext context,
               IInstrumentService instrumentService)
               : base(configuration)
        {
            _context = context;
            _instrumentService = instrumentService;
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
        public async Task<Result> AddCashDividend(CashDividendVM model)
        {
            try
            {
               
                if (model == null)
                    return Result.Failure(new[] { "Invalid request data." });

                model.Client_Code = model.Client_Code.PadLeft(7, '0');
                bool isInstrument = false;

                if (model.Instrument_ID.HasValue)
                {
                    isInstrument = await _instrumentService.CheckInterimDividend(model.Instrument_ID.Value);
                }
                if (isInstrument)
                {
                    if (model.IsFractionDividend)
                    {
                        model.Remarks = "Interim Dividend (fraction) " + " " + model.PeriodStartDate + " - " + model.PeriodEndDate;
                    }
                    else
                    {
                        model.Remarks = "Cash Dividend (fraction) " + " " + model.PeriodStartDate + " - " + model.PeriodEndDate;
                    }
                }
                else 
                {
                    if (model.IsFractionDividend)
                    {
                        if (model.IsInterimDividend)
                        {
                            model.Remarks = "Interim Dividend (fraction) " + " " + model.PeriodStartDate + " - " + model.PeriodEndDate;
                        }
                        else
                        {
                            model.Remarks = "Cash Dividend (fraction) " + " " + model.PeriodStartDate + " - " + model.PeriodEndDate;
                        }
                    }
                    else
                    {
                        if (model.IsInterimDividend)
                        {
                            model.Remarks = "Interim Dividend " + " " + model.PeriodStartDate + " - " + model.PeriodEndDate;
                        }
                        else
                        {
                            model.Remarks = "Cash Dividend " + " " + model.PeriodStartDate + " - " + model.PeriodEndDate;
                        }
                    }
                }

                var saveresult = await SaveCashDividend(model);

                return saveresult;
            }
            catch (Exception ex)
            {
                return Result.Failure(new List<string> { ex.Message });
            }
        }

        public async Task<Result> SaveCashDividend(CashDividendVM model)
        {
            string query = "SP_AddCashDividendReceiptRequest";

            var parameters = new DynamicParameters();
            parameters.Add("@Client_Code", model.Client_Code, DbType.String);
            parameters.Add("@Instrument_ID", model.Instrument_ID, DbType.Int32);
            parameters.Add("@NetAmount", model.NetAmount, DbType.Decimal);
            parameters.Add("@GrossAmount", model.GrossAmount, DbType.Decimal);
            parameters.Add("@TaxAmount", model.TaxAmount, DbType.Decimal);
            parameters.Add("@TransactionType", model.TransactionType, DbType.Int32);
            parameters.Add("@TaxRate", model.TaxRate, DbType.Decimal);
            parameters.Add("@Quantity", model.Quantity, DbType.Int32);
            parameters.Add("@AccountId", model.AccountId, DbType.Int32);
            parameters.Add("@Remarks", model.Remarks, DbType.String);
            parameters.Add("@TransactionDate", model.TransactionDate, DbType.Date);
            parameters.Add("@MatureDate", model.MatureDate, DbType.Date);
            parameters.Add("@IsApprove", model.IsApprove, DbType.Boolean);
            parameters.Add("@CreatedAt", model.CreatedAt ?? DateTime.Now, DbType.DateTime);
            parameters.Add("@CreatedBy", model.CreatedBy, DbType.Int32);
            parameters.Add("@UpdatedAt", model.UpdatedAt ?? DateTime.Now, DbType.DateTime);
            parameters.Add("@UpdatedBy", model.UpdatedBy, DbType.Int32);
            parameters.Add("@Client_Transaction_ID", model.Client_Transaction_ID, DbType.Int32);
            parameters.Add("@IsPending", model.IsPending, DbType.Boolean);
            parameters.Add("@PeriodStartDate", model.PeriodStartDate, DbType.Date);
            parameters.Add("@PeriodEndDate", model.PeriodEndDate, DbType.Date);
            parameters.Add("@message", dbType: DbType.String, direction: ParameterDirection.Output, size: 200);

            try
            {
                var result = await SetSingleAsync(query, parameters);
                return result; 
            }
            catch (Exception ex)
            {
                return Result.Failure(new List<string> { ex.Message });
            }
        }
    }
}    