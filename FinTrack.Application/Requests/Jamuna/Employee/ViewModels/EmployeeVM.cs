using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.Requests.Jamuna.Employee.ViewModels
{
    public class EmployeeVM
    {
        public int BP_ID { get; set; }
        public string Employee_Code { get; set; }
        public string Designation { get; set; }
        public bool Is_User { get; set; }
        public int User_ID { get; set; }
        public bool Is_Active { get; set; }
        public bool Is_View_All_Report { get; set; }
        public string BP_Name { get; set; }
        public string Email { get; set; }

    }
}
