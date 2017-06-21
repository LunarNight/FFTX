using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FFTX.Models;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
namespace FFTX.ModelsSql
{
    public class MailSql
    {
        //新建用户拥有站内信
        public bool creatMail(Mail m)
        {
            string sql = string.Format("INSERT INTO FFTX_Mail (user_id) VALUES ('{0}')",
                                                                        m.User_Id);
            int r = SqlDB.ExecuteNonQuery(sql);
            if (r == 1)
                return true;
            else
                return false;
        }
        public bool addCommentMail(string uid)
        {
            string sql = string.Format("update FFTX_Mail set comment_num = comment_num+1 where user_id = {0}",
                                                                        uid);
            int r = SqlDB.ExecuteNonQuery(sql);
            if (r == 1)
                return true;
            else
                return false;
        }
        public bool getMailInfo(Mail m)
        {
            string sql = string.Format("select * from FFTX_Mail where user_id = '{0}'", m.User_Id);
            SqlDataReader sqldr = SqlDB.ExecuteReader(sql);

            if (sqldr != null && sqldr.HasRows)
            {
                sqldr.Read();           //读第一行数据
                m.Like_Num = sqldr.GetInt32(2);
                m.Comment_Num = sqldr.GetInt32(3);
                m.Share_Num = sqldr.GetInt32(4);
                m.Message_Num = sqldr.GetInt32(5);
                return true;
            }
            return false;
        }
        //查看评论
        public bool lookComment(Mail m,Comment c)
        {
            
            //改变标志位
            string sql2 = string.Format("Update FFTX_Photo_Comment set comment_look_flag = 1 where comment_id = {0}",c.Comment_Id);
            int r2 = SqlDB.ExecuteNonQuery(sql2);
            //success
            if (r2 == 1)
            {
                //更新站内信条数
                string sql = string.Format("Update FFTX_Mail set comment_num = {0} where user_id = {1}", m.Comment_Num, m.User_Id);
                int r = SqlDB.ExecuteNonQuery(sql);
                if (r == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
            
        }
        //清空信息
        public bool emptyMail(Mail m)
        {
            string sql = string.Format("Update FFTX_Mail set comment_num = 0,share_num=0,like_num=0,message_num =0 where user_id = {2}", m.Comment_Num, m.Message_Num, m.User_Id);
            int r = SqlDB.ExecuteNonQuery(sql);
            //执行失败
            if (r != 1)
                return false;
            return true;
        }
    }
}