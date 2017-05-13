using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FFTX.Models
{
    public class User
    {
        public string User_Id { get; set; }
        public string User_Name { get; set; }
        public string User_Password { get; set; }
        public string Password_Question { get; set; }
        public string Password_Answer{ get; set; }
        public int User_State { get; set; }
        public string User_Email { get; set; }
        public string User_Phone { get; set; }
        public string User_HeadImg { get; set; }
        public string User_Sex { get; set; }
        public int User_Fans { get; set; } 
        public int User_Follow { get; set; }
    }
}