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
    public class AlbumSql
    {
        //创建相册
        public int addAlbum(Album album)
        {
            string sql = string.Format("INSERT INTO FFTX_Album (user_id,album_name,album_cover,album_describe,album_power) VALUES ('{0}','{1}','{2}','{3}','{4}')",
                                                          album.User_Id,album.Album_Name,album.Album_Cover,album.Album_Describe,album.Album_Power);
            
            int result = SqlDB.ExecuteNonQuery(sql);
            return result;
        }

        //重命名相册
        public bool renameAlbum(Album album)
        {
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

        //删除相册
        public bool deleteAlbum(Album album)
        {
            return true;
        }

        //得到相册信息
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

        public List<Photo> getPhotosById(Album album)
        {

            string sql = string.Format("select * from FFTX_Photo where album_id = '{0}'", album.Album_Id);

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

                    list.Add(p);
                }
                return list;
            }
            return null;
        }
    } 
}