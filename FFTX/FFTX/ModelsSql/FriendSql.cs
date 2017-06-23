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
    public class FriendSql
    {

     


       
        //  根据传入 用户组参数 进行遍历显示内容(使用方法)
        /// <summary>
        /// 获取用户所有的分组
        /// </summary>
        /// <param name="u">根据用户ID 获取好友列表  </param>
        /// <returns>返回Friend List</returns>
        public List<Friend> getFriendList(User u, int group_id)
        {
            //获取表中信息
            string sql = string.Format("SELECT * FROM FFTX_Friend WHERE user_id = {0} and group_id={1}",u.User_Id,group_id);
            SqlDataReader sqldr = SqlDB.ExecuteReader(sql);

            if (sqldr != null && sqldr.HasRows)
            {
                List<Friend> list = new List<Friend>();
                while (sqldr.Read())    //读数据
                {
                    Friend fr = new Friend(); //获取对象来存放数据
                    fr.User_Id = sqldr.GetValue(1)+"";
                    fr.Follow_Id = sqldr.GetValue(2) + "";
                    fr.Group_Id = sqldr.GetInt32(3);
                    fr.Follow_Id_Remark = sqldr.GetValue(4) + "";
                    
                    //应该根据分组不同  转换成不同的  list 暂时不搞
                    //或者遍历list 只显示某一分组的好友
                    list.Add(fr);
                }
                return list;
            }
            return null;

        
        }

        /// <summary>
        /// 是否是好友
        /// </summary>
        /// <param name="f">需要 user_id 和 follow_id</param>
        /// <returns>有关系返回true(获取到相关信息)</returns>
        public bool hasBeenFriend(Friend f)
        {
            //查找相关信息
            string sql = string.Format("select * from FFTX_Friend where user_id='{0}' and follow_id='{1}'",f.User_Id,f.Follow_Id);
            SqlDataReader sqldr = SqlDB.ExecuteReader(sql);
            if (sqldr != null && sqldr.HasRows)
            {
                sqldr.Read();
                f.Group_Id = sqldr.GetInt32(3);
                f.Follow_Id_Remark = sqldr.GetValue(4) + "";
                return true;
            }
            else
            {
                return false;
            }
            
        }
        

        /// <summary>
        /// 添加好友 好友所在组 人员数+1
        /// </summary>
        /// <param name="f">包含User Id, Follow_Id, Group_Id, Follow_id_Remark</param>
        /// <returns>成功返回1  已成为好友返回2 失败返回0</returns>
        public int addFriend(Friend f)
        {

            //添加之前先查询是否已经成为好友
            if (hasBeenFriend(f))
            {
                return 2;
            }
            else
            {   // 没有成为好友  再添加好友
                string sql = string.Format("INSERT INTO FFTX_Friend (user_id,follow_id,group_id,follow_id_remark) VALUES ('{0}','{1}','{2}','{3}')",
                                                           f.User_Id, f.Follow_Id, f.Group_Id, f.Follow_Id_Remark);
                int result = SqlDB.ExecuteNonQuery(sql);

                //用户关注和 粉丝数的变更
                //加完好友  好友所在组数+1
                if(result==1 && new GroupSql().plusOneInGroupNum(f) && new UserSql().changeUserFans(f.Follow_Id,"plus") && new UserSql().changeUserFollow(f.User_Id,"plus"))
                    return 1;
                else
                    return 0;
            }
           
        }
        //删除好友
        public int deleteFriend(Friend f)
        {

            //删除好友
            string sql = string.Format("DELETE FROM FFTX_Friend WHERE user_id={0} AND follow_id={1}",f.User_Id,f.Follow_Id);

            int result = SqlDB.ExecuteNonQuery(sql);
            //用户 粉丝 与 关注数变化

            //好友所在分组总数 -1
            if (result == 1 && new GroupSql().decOneInGroupNum(f) && new UserSql().changeUserFans(f.Follow_Id, "dec") && new UserSql().changeUserFollow(f.User_Id, "dec"))
                return 1;
            else
                return 0;
        }
        //查找好友
        public User getFriendInfo(User user)
        {
            string sql = string.Format("select * from FFTX_User where user_id = '{0}'", user.User_Id);
            SqlDataReader sqldr = SqlDB.ExecuteReader(sql);

            if (sqldr != null && sqldr.HasRows)
            {
                sqldr.Read();           //读第一行数据

                User u = new User();    //获取对象来存放数据
                u.User_Id = sqldr.GetValue(0) + "";
                u.User_Name = sqldr.GetValue(1) + "";
                u.User_State = sqldr.GetInt32(5);
                u.User_Email = sqldr.GetValue(6) + "";
                u.User_Phone = sqldr.GetValue(7) + "";
                u.User_HeadImg = sqldr.GetValue(8) + "";
                u.User_Sex = sqldr.GetValue(9) + "";
                u.User_Fans = sqldr.GetInt32(10);
                u.User_Follow = sqldr.GetInt32(11);

                return u;
            }
            return null;
        }
    }
}