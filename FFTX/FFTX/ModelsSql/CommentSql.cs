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
            return true;
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
            return null;
        }

    }
}