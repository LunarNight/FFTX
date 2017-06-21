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
    public class AdminSql
    {
        //验证管理员登录
        public bool Login(Admin a)
        {
            bool result = false;
            string sql = string.Format("select * from FFTX_Admin where admin_id = '{0}'and admin_password = '{1}'", a.Admin_Id,a.Admin_Password);
            SqlDataReader sqldr = SqlDB.ExecuteReader(sql);
            if (sqldr != null && sqldr.HasRows)
            {
                //获取 管理员姓名
                sqldr.Read();
                a.Admin_name = sqldr.GetValue(2) + "";
                result = true;
            }
            return result;
        }

        // 获取 用户所有请求
        // request state  0代表未处理   1代表已拒绝 删除则表明解封成功
        public List<UserRequest> getUserRequst()
        {
            string sql = string.Format("select * from FFTX_Admin_Request where request_state = 0");
            SqlDataReader sqldr = SqlDB.ExecuteReader(sql);
            
            if(sqldr != null && sqldr.HasRows)
            {
                
                List<UserRequest> list = new List<UserRequest>();
                //获取user request
                while (sqldr.Read())
                {
                    UserRequest ur = new UserRequest();
                    ur.User_Id = sqldr.GetValue(1) + "";
                    ur.Request_Time = sqldr.GetDateTime(2);

                    list.Add(ur);
                }
                return list;
            }

            return null;
        }
       
        //主动封号
        public bool blockUser(AdminOperate ao)
        {
            bool result = false;
            User u = new User();
            u.User_Id = ao.User_Id;
            //判断用户是否存在
            if (new UserSql().isExistUser(u))
            {
                //改用户状态
                string sql = string.Format("Update FFTX_User set user_state = 0 where user_id = {0}", ao.User_Id);
                int r = SqlDB.ExecuteNonQuery(sql);
                //执行失败
                if (r != 1)
                    return false;
                
                //添加一条 处理数据
                string sql2 = string.Format("INSERT INTO FFTX_Admin_Operate (admin_id,user_id,operate_time,operate_reason) VALUES ('{0}','{1}','{2}','{3}')",
                                                                      ao.Admin_Id,ao.User_Id,ao.Operate_Time,ao.Operate_Reason);
                r = SqlDB.ExecuteNonQuery(sql2);
                //添加数据失败
                if (r != 1)
                    return false;

                return true;
            }
            
            return result;
        }

        // 处理用户请求
        public bool operateRequest(){
            return true;
        }
        public AdminOperate getAdminOperateByuid(User u)
        {
            AdminOperate ao = new AdminOperate();
            string sql = string.Format("select top 1  * from FFTX_Admin_Operate where user_id = {0} order by operate_id", u.User_Id);
            SqlDataReader sqldr = SqlDB.ExecuteReader(sql);
            if (sqldr != null && sqldr.HasRows)
            {
                sqldr.Read();
                ao.Admin_Id = sqldr.GetValue(1)+"";
                ao.User_Id = sqldr.GetValue(2) + "";
                ao.Operate_Time = sqldr.GetDateTime(3);
                ao.Operate_Reason = sqldr.GetValue(4) + "";
                return ao;
            }
            return null;
        }
        
    }
}