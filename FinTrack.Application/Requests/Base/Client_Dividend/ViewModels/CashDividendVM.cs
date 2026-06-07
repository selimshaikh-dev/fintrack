using FinTrack.Application.AuthUser.Commands;
using FinTrack.Application.AuthView.Commands;
using FinTrack.Application.Commands.Base.ClientCashDividend;
using FinTrack.Application.Common.Mappings;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.Requests.Base.Client_Dividend.ViewModels
{
    public class CashDividendVM : IMapFrom<AddCashDividendCommand>
    {
        public int Id { get; set; }
        public int? BP_ID { get; set; }
        public string? Client_Code { get; set; }
        public int? Instrument_ID { get; set; }
        public string Instrument_ID_DSE { get; set; }
        public decimal NetAmount { get; set; }
        public decimal GrossAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public int TransactionType { get; set; }
        public decimal? TaxRate { get; set; }
        public int? Quantity { get; set; }
        public int? AccountId { get; set; }
        public string Remarks { get; set; }
        public DateTime TransactionDate { get; set; }
        public DateTime MatureDate { get; set; }
        public bool IsApprove { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? UpdatedBy { get; set; }
        public int? Client_Transaction_ID { get; set; }
        public bool? IsPending { get; set; }
        public DateTime? PeriodStartDate { get; set; }
        public DateTime? PeriodEndDate { get; set; }
        public string Message { get; set; }
        public bool IsInterimDividend { get; set; }
        public bool IsFractionDividend { get; set; }
    }
}