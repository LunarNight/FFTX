using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FFTX.Models
{
    public class AdminOperate
    {
        public string Admin_Id { get; set; }
        public string User_Id { get; set; }
        public DateTime Operate_Time { get; set; }
        public string Operate_Reason { get; set; }
    }
}