using FinTrack.Domain.Entities.Auth.AuthUser;
using FinTrack.Domain.Entities.Auth.AuthViews;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace FinTrack.Domain.Entities.Mapping
{
    public class UserToMemberMapping
    {
        [Key]
        public int Mapping_ID { get; set; }
        public string User_ID { get; set; }
        public int BP_ID { get; set; }
        public bool Is_Active { get; set; }
        public string Status { get; set; }
        public Nullable<System.DateTime> Request_Date { get; set; }
        public Nullable<System.DateTime> Processed_Date { get; set; }
        public string Processed_By { get; set; }
        public bool Is_Primary { get; set; }
        public Nullable<System.DateTime> Updated_At { get; set; }
        public string Updated_By { get; set; }
        public bool Is_Suspended { get; set; }
        public Nullable<short> LinkedAs { get; set; }

    }
}

