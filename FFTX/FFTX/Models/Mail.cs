using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FFTX.Models
{
    public class Mail
    {
        public string User_Id { get; set; }
        public int Like_Num { get; set; }
        public int Comment_Num { get; set; }
        public int Share_Num { get; set; }
        public int Message_Num { get; set; } 
    }
}