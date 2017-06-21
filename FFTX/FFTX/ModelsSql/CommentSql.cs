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
    public class CommentSql
    {
        //评论
        public bool addComment(Comment c)
        {
            string sql = string.Format("INSERT INTO FFTX_Photo_Comment (comment_user_id,comment_user_name,photo_id,comment_content,reply_id,comment_time,reply_user_id,reply_user_name) VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')",
                                                          c.Comment_User_Id,c.Comment_Name,c.Photo_Id,c.Comment_Content,c.Reply_Id,c.Comment_Time,c.Reply_User_Id,c.Reply_User_Name);
            int result = SqlDB.ExecuteNonQuery(sql);
            if (result == 1)
                return true;
            else
                return false;


        }
        //删除
        public bool deleteComment(Comment c)
        {
            return true;
        }
        
        /// <summary>
        /// 获得此照片的所有评论
        /// </summary>
        /// <param name="photo_id">相片id</param>
        /// <returns>成功则返回 一个List</returns>
        public List<Comment> getComments(int photo_id)
        {
            string sql = string.Format("select * from FFTX_Photo_Comment where photo_id = {0}",photo_id);
            SqlDataReader sqldr = SqlDB.ExecuteReader(sql);

            if (sqldr != null && sqldr.HasRows)
            {

                List<Comment> list = new List<Comment>();
                //获取user request
                while (sqldr.Read())
                {
                    Comment c = new Comment();
                    c.Comment_Id = sqldr.GetInt32(0);
                    c.Comment_User_Id = sqldr.GetValue(1)+ "";
                    c.Comment_Name = sqldr.GetValue(2) + "";
                    c.Photo_Id = sqldr.GetInt32(3);
                    c.Comment_Content = sqldr.GetValue(4) + "";
                    c.Reply_Id = sqldr.GetInt32(5);
                    c.Reply_User_Id = sqldr.GetValue(6) + "";
                    c.Reply_User_Name = sqldr.GetValue(7) + "";
                    c.Comment_Time = sqldr.GetDateTime(8);

                    list.Add(c);
                }
                return list;
            }
            return null;
        }
        public List<Comment> getMailComments(string uid)
        {
            string sql = string.Format("select * from FFTX_Photo_Comment where reply_user_id = {0} and comment_look_flag=0", uid);
            SqlDataReader sqldr = SqlDB.ExecuteReader(sql);

            if (sqldr != null && sqldr.HasRows)
            {

                List<Comment> list = new List<Comment>();
                //获取user request
                while (sqldr.Read())
                {
                    Comment c = new Comment();
                    c.Comment_Id = sqldr.GetInt32(0);
                    c.Comment_User_Id = sqldr.GetValue(1) + "";
                    c.Comment_Name = sqldr.GetValue(2) + "";
                    c.Photo_Id = sqldr.GetInt32(3);
                    c.Comment_Content = sqldr.GetValue(4) + "";
                    c.Reply_Id = sqldr.GetInt32(5);
                    c.Reply_User_Id = sqldr.GetValue(6) + "";
                    c.Reply_User_Name = sqldr.GetValue(7) + "";
                    c.Comment_Time = sqldr.GetDateTime(8);
                    list.Add(c);
                }
                return list;
            }
            return null;
        }
        public Photo getPhotoInfoByCommentId(int cid)
        {
            string sql = string.Format("select photo_id from FFTX_Photo_Comment where comment_id = {0}", cid);
            SqlDataReader sqldr = SqlDB.ExecuteReader(sql);
            if (sqldr != null && sqldr.HasRows)
            {
                sqldr.Read();
                Photo p = new Photo();
                p.Photo_Id = sqldr.GetInt32(0);
                return p;
            }
            return null;
        }
    
    }
}