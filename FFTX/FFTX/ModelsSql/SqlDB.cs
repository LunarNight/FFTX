using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
namespace FFTX.ModelsSql
{
    public class SqlDB
    {
        protected static SqlConnection conn;
        //打开连接
        public static bool OpenConnection()
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            try
            {
                bool result = true;
                if (conn.State.ToString() != "Open")
                {
                    conn.Open();
                }
                return result;
            }
            catch (SqlException ex)
            {
                return false;
            }
        }       
        //关闭连接
        public static bool CloseConnection()
        {
            try
            {
                conn.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 执行SQL语句 update insert delete
        /// </summary>
        /// <param name="sql">需要执行的SQL语句</param>
        /// <returns>该命令影响的行数</returns>
        public static int ExecuteNonQuery(string sql)
        {
            OpenConnection();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            int retval = cmd.ExecuteNonQuery();
            CloseConnection();
            return retval;
        }


        /// <summary>
        /// 执行select语句
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <returns>返回SqlDataReader类型,可以调用此方法后进行遍历来获取</returns>
        public static SqlDataReader ExecuteReader(string sql)
        {
            try
            {
                OpenConnection();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = sql;
                SqlDataReader datareader = cmd.ExecuteReader();
                if (datareader.HasRows)
                    return datareader;
                else
                    return null;
            }
            catch
            {
                if (conn != null)
                    CloseConnection();
                return null;
            }
        }
        
    }
}