using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FFTX.Models
{
    public class Comment
    {
        public int Comment_Id { get; set; } //主键ID
        public string Comment_User_Id { get; set; }     //用户ID
        public string Comment_Name { get; set; }   //用户名
        public int Photo_Id { get; set; }       //评论的照片ID
        public int Reply_Id { get; set; }       //回复  0即是照片  否则是Comment ID
        public string Reply_User_Id { get; set; } //回复的好友id
        public string Reply_User_Name { get; set; }//回复的好友名称
        public string Comment_Content { get; set; }
        public DateTime Comment_Time { get; set; }
        public int Comment_Flag { get; set; }   //默认是0 未查看
    }
}