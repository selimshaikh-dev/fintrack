using FinTrack.Application.Common.Models;
using MediatR;
using System;

namespace FinTrack.Application.Commands.Base.ClientCashDividend
{
    public class AddCashDividendCommand : IRequest<Result>
    {
        public string Client_Code { get; set; }
        public int Instrument_ID { get; set; }
        public decimal NetAmount { get; set; }
        public decimal GrossAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public int TransactionType { get; set; }
        public decimal? TaxRate { get; set; }
        public int? Quantity { get; set; }
        public int AccountId { get; set; }
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
        public bool IsInterimDividend { get; set; }
        public bool IsFractionDividend { get; set; }

        public AddCashDividendCommand(string client_Code, int instrumentId, decimal netAmount, decimal grossAmount, decimal taxAmount,
                                      int transactionType, decimal? taxRate, int? quantity, int accountId, string remarks,
                                      DateTime transactionDate, DateTime matureDate, bool isApprove,
                                      DateTime? createdAt, int? createdBy, DateTime? updatedAt, int? updatedBy,
                                      int? clientTransactionId, bool? isPending, DateTime? periodStartDate, DateTime?periodEndDate, bool isInterimDividend, bool isFractionDividend)
        {
            Client_Code = client_Code;
            Instrument_ID = instrumentId;
            NetAmount = netAmount;
            GrossAmount = grossAmount;
            TaxAmount = taxAmount;
            TransactionType = transactionType;
            TaxRate = taxRate;
            Quantity = quantity;
            AccountId = accountId;
            Remarks = remarks;
            TransactionDate = transactionDate;
            MatureDate = matureDate;
            IsApprove = isApprove;
            CreatedAt = createdAt;
            CreatedBy = createdBy;
            UpdatedAt = updatedAt;
            UpdatedBy = updatedBy;
            Client_Transaction_ID = clientTransactionId;
            IsPending = isPending;
            PeriodStartDate = periodStartDate;
            PeriodEndDate = periodEndDate;
            IsInterimDividend = isInterimDividend;
            IsFractionDividend = isFractionDividend;
        }
    }
}