using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FFTX.Models;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
namespace FFTX.ModelsSql
{
    public class AlbumSql
    {
        //创建相册
        public int addAlbum(Album album)
        {
            string sql = string.Format("INSERT INTO FFTX_Album (user_id,album_name,album_describe,album_power) VALUES ('{0}','{1}','{2}','{3}')",
                                                          album.User_Id,album.Album_Name,album.Album_Describe,album.Album_Power);
            
            int result = SqlDB.ExecuteNonQuery(sql);
            return result;
        }

        //重命名相册
        public bool renameAlbum(Album album)
        {
            string sql = string.Format("Update FFTX_Album set album_name = '{0}' where album_id = {1}",album.Album_Name,album.Album_Id);
            int r = SqlDB.ExecuteNonQuery(sql);
            //执行失败
            if (r != 1)
                return false;
            return true;
        }
        //设置相册封面
        public bool changeAlbumCover(Album album)
        {
            string sql = string.Format("Update FFTX_Album set album_cover = '{0}' where album_id = {1}", album.Album_Cover, album.Album_Id);
            int r = SqlDB.ExecuteNonQuery(sql);
            //执行失败
            if (r != 1)
                return false;
            else
                return true;
            
        }
        //相册是否存在(好像不需要  文档没有此方法) 是否要验证相册和用户对应  防止恶意链接查看相册 
        public bool isExitAlbum(Album album)
        {

            string sql = string.Format("select album_id from FFTX_ALBUM where album_id ={0}", album.Album_Id);
            SqlDataReader sqldr = SqlDB.ExecuteReader(sql);
            if (sqldr != null && sqldr.HasRows)
            {

            }
            return false;

        }

        //删除相册              path: Request.MapPath("~s");
        public bool deleteAlbum(Album album,string path)
        {
            //查询相册中所包含的 photo id
            string sql = string.Format("select photo_id from FFTX_Photo where album_id = '{0}'", album.Album_Id);
            SqlDataReader sqldr = SqlDB.ExecuteReader(sql);
            if (sqldr != null && sqldr.HasRows)
            {
                while (sqldr.Read())
                {
                    //删除photo
                    deletePhoto(sqldr.GetInt32(0),path);
                }

            }
            string sql2 = string.Format("delete from FFTX_Album where album_id = {0}",album.Album_Id);
            int result = SqlDB.ExecuteNonQuery(sql2);
            if(result==1){
                return true;
            }
            return false;
        }
        //删除一张照片 根据photo id     string path = Request.MapPath("~");
        public bool deletePhoto(int pid, string path)
        {
            Photo p = new Photo();
            PhotoSql ps = new PhotoSql();
            p.Photo_Id = pid;
            //获取照片信息
            ps.getPhotoInfo(p);
            //删除 有关此照片的所有信息(分享 评论 点赞)
            bool result = ps.deletePhoto(p);

            //删除图片文件
            if (result)
            {
                //获取项目运行路径
                FileInfo file = new FileInfo(path + p.Photo_Src);
                if (file.Exists)
                {
                    file.Delete();
                    return true;
                }
            }
            return false;
        }


        public Album getAlbumInfoById(Album album)
        {
            string sql = string.Format("select * from FFTX_Album where album_id = '{0}'", album.Album_Id);
            SqlDataReader sqldr = SqlDB.ExecuteReader(sql);
            if (sqldr != null && sqldr.HasRows)
            {
                sqldr.Read();   //读数据
                Album al = new Album(); //获取对象来存放数据
                al.Album_Id = sqldr.GetInt32(0);
                al.User_Id = sqldr.GetValue(1) + "";
                al.Album_Name = sqldr.GetValue(2) + "";
                al.Album_Cover = sqldr.GetValue(3) + "";
                al.Album_Describe = sqldr.GetValue(4) + "";
                al.Album_Power = sqldr.GetInt32(5);
                return al;
            }
            return null;
        }
        //得到用户所有相册信息
        public List<Album> getAlbumInfo(Album album)
        {
            string sql = string.Format("select * from FFTX_Album where user_id = '{0}'", album.User_Id);

            SqlDataReader sqldr = SqlDB.ExecuteReader(sql);


            if (sqldr != null && sqldr.HasRows)
            {
                List<Album> list = new List<Album>();
                while (sqldr.Read())    //读数据
                {
                    Album al = new Album(); //获取对象来存放数据
                    al.Album_Id = sqldr.GetInt32(0);
                    al.User_Id = sqldr.GetValue(1) + "";
                    al.Album_Name = sqldr.GetValue(2) + "";
                    al.Album_Cover = sqldr.GetValue(3) + "";
                    al.Album_Describe = sqldr.GetValue(4) + "";
                    al.Album_Power = sqldr.GetInt32(5);

                    list.Add(al);
                }
                return list;
            }
            return null;
        }
        //得到好友的相册信息(权限）
        public List<Album> getFriendAlbumInfo(Album album)
        {
            string sql = string.Format("select * from FFTX_Album where user_id = '{0}' and album_power=0", album.User_Id);

            SqlDataReader sqldr = SqlDB.ExecuteReader(sql);


            if (sqldr != null && sqldr.HasRows)
            {
                List<Album> list = new List<Album>();
                while (sqldr.Read())    //读数据
                {
                    Album al = new Album(); //获取对象来存放数据
                    al.Album_Id = sqldr.GetInt32(0);
                    al.User_Id = sqldr.GetValue(1) + "";
                    al.Album_Name = sqldr.GetValue(2) + "";
                    al.Album_Cover = sqldr.GetValue(3) + "";
                    al.Album_Describe = sqldr.GetValue(4) + "";
                    al.Album_Power = sqldr.GetInt32(5);

                    list.Add(al);
                }
                return list;
            }
            return null;
        }

        public List<Photo> getPhotosById(Album album,int page)
        {
            int pagenum = (page-1) * 10;
            string sql = string.Format("SELECT TOP 10 * FROM FFTX_Photo WHERE album_id={0} and photo_id NOT IN (SELECT TOP {1} photo_id FROM FFTX_Photo where album_id={0} ORDER BY photo_id)ORDER BY photo_id", album.Album_Id,pagenum);

            SqlDataReader sqldr = SqlDB.ExecuteReader(sql);
            if (sqldr != null && sqldr.HasRows)
            {
                List<Photo> list = new List<Photo>();
                while (sqldr.Read())    //读数据
                {
                    Photo p = new Photo(); //获取对象来存放数据
                    p.Photo_Id = sqldr.GetInt32(0);
                    p.Photo_Src = sqldr.GetValue(1) + "";
                    p.Photo_Time = sqldr.GetDateTime(2);
                    p.album_id = sqldr.GetInt32(3);
                    p.Photo_Describe = sqldr.GetValue(4) + "";
                    p.Photo_Like = sqldr.GetInt32(5);
                    p.Photo_Label = sqldr.GetInt32(6);
                    if (p.Photo_Src.Split('(').Length > 1)
                        p.Photo_Name = p.Photo_Src.Split('(')[1].Split('.')[0];
                    else
                        p.Photo_Name = "未命名";
                    list.Add(p);
                }
                return list;
            }
            return null;
        }
        public int getPageNum(Album album)
        {
            string sql = string.Format("SELECT COUNT(0) from FFTX_Photo where album_id ={0}", album.Album_Id);
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