using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FFTX.Models
{
    public class Album
    {
        public int Album_Id { get; set; }
        public string User_Id { get; set; }
        public string Album_Name { get; set; }
        public string Album_Cover { get; set; }
        public string Album_Describe { get; set; }
        public int Album_Power { get; set; }

    }
}