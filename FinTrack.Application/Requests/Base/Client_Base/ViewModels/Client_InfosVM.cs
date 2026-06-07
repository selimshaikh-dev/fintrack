using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.Requests.Base.Client_Base.ViewModels
{
    public class Client_InfosVM
    {
        public int BP_ID { get; set; }
        public string Client_Code { get; set; }
        public int Client_Group_ID { get; set; }
        public int Client_Type { get; set; }
        public string BO_ID_DSE { get; set; }
        public string BP_Name { get; set; }
        public string Address_Line_1 { get; set; }
        public string Address_Line_2 { get; set; }
        public string WorkStationCode { get; set; }
        public int WorkStationID { get; set; }
        public int Current_Branch_ID { get; set; }
        public int Opening_Branch_ID { get; set; }
        public int ClientGroupID { get; set; }
        public bool IsVIPClient { get; set; }
        public int Mapped_User_Id { get; set; }
        public string Opening_Branch { get; set; }
        public string Current_Branch { get; set; }
        public bool Is_Dormant { get; set; }
        public bool Is_Continued { get; set; }
        public bool IS_Suspended { get; set; }
        public bool Is_Margin { get; set; }
        public bool Is_Cash { get; set; }
        public DateTime Bo_Opening_Date { get; set; }
        public DateTime Acc_Opening_Date { get; set; }
        public string Account_Type { get; set; }
        public decimal Commission_On_CDBL { get; set; }
        public decimal Custom_IPO_Commission { get; set; }
        public string TypeNameMarketing { get; set; }
        public string Father_Name { get; set; }
        public string Mother_Name { get; set; }
        public string TIN { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string NID { get; set; }
    }
}
