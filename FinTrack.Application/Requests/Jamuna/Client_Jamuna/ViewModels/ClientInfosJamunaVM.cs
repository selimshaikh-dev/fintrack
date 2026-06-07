using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.Requests.Jamuna.Client_Jamuna.ViewModels
{
    public class ClientInfosJamunaVM
    {
        public int BP_ID_Jamuna { get; set; }
        public bool Is_Continued { get; set; }
        public string JamunaMemberID { get; set; }
        public string JamunaMemberCode { get; set; }
        public int Loan_Type { get; set; }
        public string LoanTypeDescription { get; set; }
        public string ClientCode { get; set; }
        public string BO_ID_DSE { get; set; }
        public string Client_Group_ID { get; set; }
        public bool Is_Margin { get; set; }
        public bool Is_Cash { get; set; }
        public string BP_Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address_Line_1 { get; set; }
        public string Address_Line_2 { get; set; }
        public int Opening_Branch_ID { get; set; }
        public bool Is_Long_Term { get; set; }
        public string Father_Name { get; set; }
        public string Mother_Name { get; set; }
        public string Account_Type { get; set; }
        public DateTime Acc_Opening_Date { get; set; }
        public decimal PenWarLTV { get; set; }
        public decimal MarCalLTV { get; set; }
        public decimal MarCalTargetLTV { get; set; }
        public decimal LiqLTV { get; set; }
        public decimal LiqTargetLTV { get; set; }
        public string Type_Name_Marketing { get; set; }
        public decimal Authorized_LTV { get; set; }
        public decimal Penal_Fee_Start_LTV { get; set; }
        public decimal AMR { get; set; }
        public bool IsActive { get; set; }
        public decimal MML { get; set; }
        public bool Is_JSCC_Member { get; set; }

    }
}
