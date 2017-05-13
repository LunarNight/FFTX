using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FFTX.Models
{
    public class Request
    {
        public string User_Id { set; get; }
        public DateTime Request_Time { set; get; }
        public int Request_State { set; get; }
    }
}