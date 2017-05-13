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
        //上传照片
        public bool uploadPhoto(Photo p)
        {
            return true;
        }
        //删除
        public bool deletePhoto(Photo p)
        {
            return true;
        }
        //重命名
        public bool renamePhoto(Photo p)
        {
            return true;
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