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
            return true;
        }
        //删除留言
        public bool deleteMessage(Message m)
        {
            return true;
        }
        
        /// <summary>
        /// 获得所有留言内容
        /// </summary>
        /// <param name="user_id">用户ID</param>
        /// <returns>留言列表</returns>
        public List<Message> getMessage(int user_id)
        {
            return null;
        }
    }
}