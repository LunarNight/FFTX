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
    public class PhotoSql
    {
        //获取照片信息
        public bool getPhotoInfo(Photo p)
        {
            string sql = String.Format("select * from FFTX_Photo where photo_id = '{0}'", p.Photo_Id);
            SqlDataReader sqldr = SqlDB.ExecuteReader(sql);
            if (sqldr != null && sqldr.HasRows)
            {
                sqldr.Read();           //读第一行数据
                p.Photo_Id = sqldr.GetInt32(0);
                p.Photo_Src = sqldr.GetValue(1) + "";
                p.Photo_Time = sqldr.GetDateTime(2);
                p.album_id = sqldr.GetInt32(3);
                p.Photo_Describe = sqldr.GetValue(4) + "";
                p.Photo_Like = sqldr.GetInt32(5);
                p.Photo_Label = sqldr.GetInt32(6);
                return true;
            }
            return false;
        }
        //上传照片
        public bool uploadPhoto(Photo p)
        {
            string sql = string.Format("INSERT INTO FFTX_Photo (photo_src,photo_time,album_id,photo_label) VALUES ('{0}','{1}','{2}','{3}')",
                                                         p.Photo_Src,p.Photo_Time,p.album_id,p.Photo_Label);
            //在Photo表中添加一条信息
            int result = SqlDB.ExecuteNonQuery(sql);
            if (result == 1)
                return true;
            else
                return false;
            
        }
        //删除
        public bool deletePhoto(Photo p)
        {   
            
            string sql = string.Format("delete from FFTX_Photo where photo_id = {0}",p.Photo_Id );
            int result = SqlDB.ExecuteNonQuery(sql);
            if (result == 1){
                string sql1 = string.Format("delete from FFTX_Photo_Comment where photo_id = {0}",p.Photo_Id );
                string sql2 = string.Format("delete from FFTX_Photo_Like where photo_id = {0}", p.Photo_Id);
                string sql3 = string.Format("delete from FFTX_Photo_Share where photo_id = {0}", p.Photo_Id);
                SqlDB.ExecuteNonQuery(sql1);
                SqlDB.ExecuteNonQuery(sql2);
                SqlDB.ExecuteNonQuery(sql3);


                return true;
            }
                
            else
                return false;
           
        }
        //重命名
        public bool renamePhoto(Photo p)
        {
            string sql = string.Format("Update FFTX_Photo set photo_src = '{0}' where photo_id = {1}", p.Photo_Src, p.Photo_Id);
            int result = SqlDB.ExecuteNonQuery(sql);
            if (result == 1)
                return true;
            else
                return false;
        }
        //点赞
        public bool likePhoto(int photo_id,string user_id,string user_name)
        {
            //对点赞表进行操作
            return true;
        }
        //裁剪
        public bool cutPhoto(Photo p)
        {
            return true;
        }
        
        //滤镜
        public bool addFilter(Photo p )
        {
            return true;
        }
        //水印
        public bool waterMark(Photo p)
        { 
            return true;
        }
        //获取此照片的用户信息
        public User getUserByPhoto(Photo p)
        {
            string sql = string.Format("select u.user_id,u.user_name from FFTX_User u,FFTX_Album a,FFTX_Photo p Where a.album_id = p.album_id AND a.user_id = u.user_id AND p.photo_id = {0}",p.Photo_Id);
            SqlDataReader sqldr = SqlDB.ExecuteReader(sql);
            if (sqldr != null && sqldr.HasRows)
            {
                sqldr.Read();
                User u = new User();
                u.User_Id = sqldr.GetValue(0) + "";
                u.User_Name = sqldr.GetValue(1) + "";
                return u;
            }
            return null;
        }
        //排行榜 5个图片
        public List<Photo> getRankList()
        {

            string sql = string.Format("select top 5 * from FFTX_Photo ORDER BY photo_like desc");
            SqlDataReader sqldr = SqlDB.ExecuteReader(sql);
            if (sqldr != null && sqldr.HasRows)
            {
                List<Photo> list = new List<Photo>();
                while (sqldr.Read())
                {
                    Photo p = new Photo();
                    p.Photo_Id = sqldr.GetInt32(0);
                    p.Photo_Src = sqldr.GetValue(1) + "";
                    p.Photo_Time = sqldr.GetDateTime(2);
                    p.album_id = sqldr.GetInt32(3);
                    p.Photo_Describe = sqldr.GetValue(4) + "";
                    p.Photo_Like = sqldr.GetInt32(5);
                    p.Photo_Label = sqldr.GetInt32(6);
                    list.Add(p);
                }
                return list;
            }
            return null;
        }
        //推荐图片 5个图片
        public List<Photo> getRecommandList(User u)
        {
            string sql = string.Format("select top 5 p.photo_id,p.photo_src,p.photo_time,p.album_id,photo_describe,p.photo_like,p.photo_label from FFTX_Photo p,FFTX_User u,FFTX_Album a where photo_label = (SELECT top 1 photo_label FROM FFTX_Photo p,FFTX_Album a where a.user_id={0} and a.album_id = p.album_id and p.photo_label<>1 and p.photo_label<>2 and p.photo_label<>3 GROUP BY photo_label ORDER BY count( * ) DESC)and u.user_id<> {0} and u.user_id= a.user_id and a.album_id = p.album_id order by newid()", u.User_Id);
            SqlDataReader sqldr = SqlDB.ExecuteReader(sql);
            if (sqldr != null && sqldr.HasRows)
            {
                List<Photo> list = new List<Photo>();
                while (sqldr.Read())
                {
                    Photo p = new Photo();
                    p.Photo_Id = sqldr.GetInt32(0);
                    p.Photo_Src = sqldr.GetValue(1) + "";
                    p.Photo_Time = sqldr.GetDateTime(2);
                    p.album_id = sqldr.GetInt32(3);
                    p.Photo_Describe = sqldr.GetValue(4) + "";
                    p.Photo_Like = sqldr.GetInt32(5);
                    p.Photo_Label = sqldr.GetInt32(6);
                    list.Add(p);
                }
                return list;
            }
            return null;
        }

        //搜索照片
        public List<Photo> searchName(string name)
        {
            string sql = string.Format("select * from FFTX_Photo p,FFTX_Album a where p.photo_src like '%(%{0}%' and a.album_power = 0 and a.album_id = p.album_id;", name);
            SqlDataReader sqldr = SqlDB.ExecuteReader(sql);
            if (sqldr != null && sqldr.HasRows)
            {
                List<Photo> list = new List<Photo>();
                while (sqldr.Read())
                {
                    Photo p = new Photo();
                    p.Photo_Id = sqldr.GetInt32(0);
                    p.Photo_Src = sqldr.GetValue(1) + "";
                    p.Photo_Time = sqldr.GetDateTime(2);
                    p.album_id = sqldr.GetInt32(3);
                    p.Photo_Describe = sqldr.GetValue(4) + "";
                    p.Photo_Like = sqldr.GetInt32(5);
                    p.Photo_Label = sqldr.GetInt32(6);
                    list.Add(p);
                }
                return list;
            }
            return null;
        }
        
    }
}