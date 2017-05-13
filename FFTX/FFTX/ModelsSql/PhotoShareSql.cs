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
    public class PhotoShareSql
    {
        //添加分享
        public bool addShare(PhotoShare ps)
        {
            return true;
        }
        //删除分享
        public bool deleteShare(PhotoShare ps)
        {
            return true;
        }
        //是否已分享
        public bool isShare(PhotoShare ps)
        {
            return true;
        }
    }
}