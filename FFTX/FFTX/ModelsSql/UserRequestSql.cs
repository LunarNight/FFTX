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
    public class UserRequestSql
    {
        //解封
        public bool deblockUser(UserRequest ur)
        {
            string sql = string.Format("delete from FFTX_Admin_Request where user_id = {0}",ur.User_Id);
            int result = SqlDB.ExecuteNonQuery(sql);
            if (result == 1)
                return true;
            else
                return false;
        }
        public bool blockUser(UserRequest ur)
        {
            string sql = string.Format("update  FFTX_Admin_Request set request_state = 1 where user_id = {0}", ur.User_Id);
            int result = SqlDB.ExecuteNonQuery(sql);
            if (result == 1)
                return true;
            else
                return false;
        }
    }
}