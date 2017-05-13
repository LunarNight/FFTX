using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FFTX.Models
{
    public class Group
    {
        public string User_Id { get; set; }
        public int Group_Id { get; set; }
        public string Group_Name { get; set; }
        public int Group_Friend_Num { get; set; }
    }
}