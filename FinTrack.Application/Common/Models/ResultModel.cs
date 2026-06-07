using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.Common.Models
{
    public class ResultModel
    {
        public string Id { get; set; }
        public bool Succeed { get; set; }
        public object Data { get; set; }
        public IList<object> DataList { get; set; }
        public string Message { get; set; }
    }
}
