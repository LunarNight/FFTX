using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FFTX.Models
{
    public class Message
    {
        public int Message_Id { get; set; }             //主键 留言ID
        public string Message_Content { get; set; }
        public DateTime Message_Time { get; set; }
        public string Message_Friend_Id { get; set; }   //朋友的留言板   用户ID
        public int Message_Reply_Id { get; set; }       //回复哪条消息   0即是留言
        public string Message_User_Id { get; set; }     //留言的用户ID
        public string Message_User_Name { get; set; }   //留言用户的名字
        public int Message_Look_Flag { get; set; }      //站内信查看标志位 默认是0 未查看
    }
}