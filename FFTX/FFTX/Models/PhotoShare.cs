using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FFTX.Models
{
    public class PhotoShare
    {
        public int Id { get; set; } //主键
        public string Photo_Share_Id { get; set; } //0即 分享到广场  否则1对1分享
        public int Photo_Id { get; set; }
        public string Photo_Src { get; set; }
        public DateTime Photo_Share_Time { get; set; }
        public int Photo_Share_Flag { get; set; }   //如果分享到广场 默认为1 已查看
        public string User_Id { get; set; }
        public string User_Name { get; set; }
    }
}