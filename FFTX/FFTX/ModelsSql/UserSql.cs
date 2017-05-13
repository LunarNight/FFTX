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
    public class UserSql
    {   

        /// <summary>
        /// 判断用户ID是否存在
        /// </summary>
        /// <param name="user">传入的用户类</param>
        /// <returns>存在就返回 true</returns>
        public bool isExistUser(User user)
        {
            if (getUserInfo(user) != null)
                return true;
            else
                return false;
        }


        /// <summary>
        /// 根据ID查找用户
        /// </summary>
        /// <param name="user">用户类</param>
        /// <returns>查找到就返回包含此用户所有信息的类变量,否则返回NULL</returns>
        public User getUserInfo(User user)
        {
            string sql = string.Format("select * from FFTX_User where user_id = '{0}'", user.User_Id);
            SqlDataReader sqldr = SqlDB.ExecuteReader(sql);
            
            if (sqldr!=null && sqldr.HasRows)
            {
                sqldr.Read();           //读第一行数据

                User u = new User();    //获取对象来存放数据
                u.User_Id = sqldr.GetValue(0)+"";
                u.User_Name = sqldr.GetValue(1) + "";
                u.User_Password = sqldr.GetValue(2) + "";
                u.Password_Question = sqldr.GetValue(3) + "";
                u.Password_Answer = sqldr.GetValue(4) + "";
                u.User_State = sqldr.GetInt32(5);
                u.User_Email = sqldr.GetValue(6)+"";
                u.User_Phone = sqldr.GetValue(7)+"";
                u.User_HeadImg = sqldr.GetValue(8)+"";
                u.User_Sex = sqldr.GetValue(9)+"";
                u.User_Fans = sqldr.GetInt32(10);
                u.User_Follow =sqldr.GetInt32(11);
    
                return u;
            }
            return null;
                
        }


        /// <summary>
        /// 用户注册 注册信息包括: ID 用户名 密码 密保问题 密保答案
        /// </summary>
        /// <param name="user">用户类</param>
        /// <returns>添加成功返回1   失败返回-1</returns>
        public int addUser(User user)
        {
            int result = -1;
            
            string sql = string.Format("INSERT INTO FFTX_User (user_id,user_name,user_password,password_question,password_answer) VALUES ('{0}','{1}','{2}','{3}','{4}')",
                                                          user.User_Id,user.User_Name,user.User_Password,user.Password_Question,user.Password_Answer);
            //判断用户ID是否存在.不存在便添加
            if (!isExistUser(user))
                result = SqlDB.ExecuteNonQuery(sql);
            
            return result;
        }


        /// <summary>
        /// 验证用户登录密码是否正确
        /// </summary>
        /// <param name="user">用户类</param>
        /// <returns>正确返回true 错误返回false</returns>
        public bool judgeUser(User user)
        {
            User u = getUserInfo(user);
            if (u!=null && u.User_Password.Equals(user.User_Password))
                return true;
            else
                return false;
        }
        //用户修改信息

        /// <summary>
        /// 改变用户粉丝数
        /// </summary>
        /// <param name="user">需要user_id</param>
        /// <param name="operate">操作命令 plus 或 dec</param>
        /// <returns>成功返回true 失败返回false</returns>
        public bool changeUserFans(string user_id,string operate)
        {
            string sql=null;
            if(operate.Equals("plus"))
            {
                sql = string.Format("Update FFTX_User set user_fans = (user_fans+1) where user_id = {0}", user_id);
            }
            else if (operate.Equals("dec"))
            {
                sql = string.Format("Update FFTX_User set user_fans = (user_fans+1) where user_id = {0}", user_id);
            }
            else
            {
                return false;
            }

            int result = SqlDB.ExecuteNonQuery(sql);
            if (result == 1)
                return true;
            else
                return false; 
        }
        public bool changeUserFollow(string user_id, string operate)
        {
            string sql = null;
            if (operate.Equals("plus"))
            {
                sql = string.Format("Update FFTX_User set user_follow = (user_follow+1) where user_id = {0}", user_id);
            }
            else if (operate.Equals("dec"))
            {
                sql = string.Format("Update FFTX_User set user_fans = (user_fans+1) where user_id = {0}", user_id);
            }
            else
            {
                return false;
            }

            int result = SqlDB.ExecuteNonQuery(sql);
            if (result == 1)
                return true;
            else
                return false; 
        }

        /// <summary>
        /// 找回密码
        /// </summary>
        /// <param name="user">User_ID Question Anwser</param>
        /// <returns>返回密码</returns>
        public string forgetPassword(User user)
        {
            string password = null;
            string sql = string.Format("select * from FFTX_User where user_id='{0}' and password_question = '{1}' and password_answer ='{2}'",user.User_Id,user.Password_Question,user.Password_Answer);
            SqlDataReader sqldr = SqlDB.ExecuteReader(sql);
            if (sqldr != null && sqldr.HasRows)
            {
                sqldr.Read();
                password = sqldr.GetValue(2) + "";
            }
            return password;
        }

        //申请解封
        // 数据库添加一条信息
        public bool requestDeblock(UserRequest ur)
        {
            string sql = string.Format("INSERT INTO FFTX_Admin_Request (user_id,request_time) VALUES ('{0}','{1}')",
                                                           ur.User_Id,ur.Request_Time);
            int result = SqlDB.ExecuteNonQuery(sql);
            if (result == 1)
                return true;
            else
                return false;
        }
        public bool changeUserState(string uid,int state)
        {
            string sql = string.Format("Update  FFTX_User set user_state = {0} where user_id = '{1}'",state,uid);
            int result = SqlDB.ExecuteNonQuery(sql);
            if (result == 1)
                return true;
            else
                return false;
        }
        //更新用户信息
        public bool updateUserInfo(User user)
        {
            string sql = string.Format("Update FFTX_User set user_name='{0}',user_email='{1}',user_phone='{2}',user_sex='{3}' where user_id = '{4}'",
                                                         user.User_Name,user.User_Email,user.User_Phone,user.User_Sex,user.User_Id);
            int result = SqlDB.ExecuteNonQuery(sql);
            if (result == 1)
                return true;
            else
                return false;
        }
        public bool changePwd(User user)
        {
            string sql = string.Format("Update FFTX_User set user_password = '{0}' where user_id = '{1}'",
                                                              user.User_Password, user.User_Id);
            int result = SqlDB.ExecuteNonQuery(sql);
            if (result == 1)
                return true;
            else
                return false;
        }
    }
}