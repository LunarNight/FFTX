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
        public bool getPhotoInfo(Photo p)
        {
            string sql = String.Format("select * from FFTX_Photo where photo_id = '{0}'", p.Photo_Id);
            SqlDataReader sqldr = SqlDB.ExecuteReader(sql);
            if (sqldr != null && sqldr.HasRows)
            {
                sqldr.Read();           //读第一行数据

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
        
    }
}