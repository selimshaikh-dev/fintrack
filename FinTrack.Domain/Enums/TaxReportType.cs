using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Domain.Enums
{
    public enum ReportType
    {
        [Description("Digital Attestation of Tax Exemption Schedule")]
        TaxExemptionSchedule = 1,
        [Description("Digital Attestation of Tax Certificate")]
        TaxCertificate = 2,
        [Description("Digital Attestation of Portfolio")]
        Portfolio = 3,
        [Description("Digital Attestation of Cosolidated Portfolio")]
        CosolidatedPortfolio = 4,
        [Description("Digital Attestation of Periodic Portfolio")]
        PeriodicPortfolio = 5,
        [Description("Digital Attestation of Ledger Details")]
        LedgerDetails = 6,
        [Description("Digital Attestation of Ledger Summary")]
        LedgerSummary = 7,
    }
}
