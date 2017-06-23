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
    public class MessageSql
    {
        //添加留言
        public bool addMessage(Message m)
        {
            string sql = string.Format("Insert into FFTX_BoardMessage (message_content,message_time,message_friend_id,message_reply_id,message_user_id,message_user_name) values('{0}','{1}','{2}',{3},'{4}','{5}')",
                                                                        m.Message_Content,m.Message_Time,m.Message_Friend_Id,m.Message_Reply_Id,m.Message_User_Id,m.Message_User_Name);
            int r = SqlDB.ExecuteNonQuery(sql);
            if (r == 1)
                return true;
            else
                return false;
        }
        //删除留言
        public bool deleteMessage(int mid)
        {
            string sql = string.Format("delete from FFTX_BoardMessage where message_id={0}", mid);
            int r = SqlDB.ExecuteNonQuery(sql);
            if (r == 1)
                return true;
            else
                return false;
        }
        
        /// <summary>
        /// 获得所有留言内容
        /// </summary>
        /// <param name="user_id">用户ID</param>
        /// <returns>留言列表</returns>
        public List<Message> getMessage(User u,int page)
        {
            int pagenum = (page - 1) * 10;
            string sql = string.Format("SELECT TOP 10 * FROM FFTX_BoardMessage WHERE message_friend_id={0} and message_id NOT IN (SELECT TOP {1} message_id FROM FFTX_BoardMessage where message_friend_id={0} ORDER BY message_id DESC)ORDER BY message_id DESC", u.User_Id, pagenum);
            SqlDataReader sqldr = SqlDB.ExecuteReader(sql);
            if (sqldr != null && sqldr.HasRows)
            {
                List<Message> list = new List<Message>();
                while (sqldr.Read())    //读数据
                {
                    Message m = new Message(); //获取对象来存放数据
                    m.Message_Id = sqldr.GetInt32(0);
                    m.Message_Content = sqldr.GetValue(1)+"";
                    m.Message_Time = sqldr.GetDateTime(2);
                    m.Message_Friend_Id = sqldr.GetValue(3) + "";
                    m.Message_Reply_Id = sqldr.GetInt32(4);
                    m.Message_User_Id = sqldr.GetValue(5) + "";
                    m.Message_User_Name = sqldr.GetValue(6) + "";
                    list.Add(m);
                }
                return list;
            }
            return null;
        }

        public int getPageNum(User u)
        {
            string sql = string.Format("SELECT COUNT(0) from FFTX_BoardMessage where message_friend_id ={0}", u.User_Id);
            SqlDataReader sqldr = SqlDB.ExecuteReader(sql);
            int pageNum = 1;
            //一页显示的数据总数
            int onePageNum = 10;
            if (sqldr != null && sqldr.HasRows)
            {
                sqldr.Read();
                int itemSum = sqldr.GetInt32(0);
                if (itemSum % onePageNum == 0)
                {
                    pageNum = (itemSum / onePageNum);
                }
                else
                {
                    pageNum = (itemSum / onePageNum) + 1;
                }
            }
            return pageNum;
        }


    }
}