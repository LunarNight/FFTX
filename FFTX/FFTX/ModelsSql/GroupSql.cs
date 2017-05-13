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
    public class GroupSql
    {
     
        /// <summary>
        /// 添加 group_name 和group_id对应信息
        /// </summary>
        /// <param name="group">需要group_name字段</param>
        /// <returns>添加成功返回true 并获取group_id</returns>
        public bool addGroupInfo(Group group)
        {
            
            //在 Group表中  添加 ID 和 组名对应信息
            if (!searchGroupInfo(group))
            {
                string sql = string.Format("Insert Into FFTX_Group(group_name) VALUES('{0}')", group.Group_Name);
                int result = SqlDB.ExecuteNonQuery(sql);

                if (result == 1)                 //添加对应信息成功
                    return searchGroupInfo(group);//获取group id是否成功
                else
                    return false;
            }
            //在Group to User表中  为用户添加相应分组
            return true;
        }

        /// <summary>
        /// 查找 group_id 和 group_name对应信息是否已存
        /// </summary>
        /// <param name="group">需要group_name字段</param>
        /// <returns>查找到返回true(并获取到group_id)</returns>
        public bool searchGroupInfo(Group group)
        {
            string sql = string.Format("SELECT * FROM FFTX_Group WHERE group_name='{0}'", group.Group_Name);
            SqlDataReader sqldr = SqlDB.ExecuteReader(sql);
            if (sqldr != null && sqldr.HasRows){
                //获取 Group ID
                sqldr.Read();
                group.Group_Id = sqldr.GetInt32(0);
                return true;
            }else
                return false;
        }

        /// <summary>
        /// 添加用户分组信息 ( 用户创建了一个分组)
        /// </summary>
        /// <param name="group">需要有 User_id和Group_name </param>
        /// <returns>添加成功返回true</returns>
        public bool addGroupFriendInfo(Group group)
        {
            //添加之前先查询是否有同名分组
            if (searchGroupFriendInfo(group))
            {
                //查找到分组 直接返回
                return true;
            }
            else
            {
                //没有查找到 添加分组
                string sql = string.Format("Insert Into FFTX_Group_to_Friend(user_id,group_id,group_name) VALUES('{0}',{1},'{2}')",group.User_Id,group.Group_Id, group.Group_Name);
                int result = SqlDB.ExecuteNonQuery(sql);
                //是否成功
                if (result == 1)
                {
                    //获取用户分组信息
                    if (searchGroupFriendInfo(group))
                        return true;
                    else
                        return false;
                }
                else
                {
                    return false;
                }
            }        
        }
         
        /// <summary>
        /// 查找用户分组信息 
        /// </summary>
        /// <param name="group">需要User id 和 Group Name</param>
        /// <returns>查找到返回true(得到完整的group信息) 没有返回false</returns>
        public bool searchGroupFriendInfo(Group group)
        {
            //是否创建好 group_id和 group_name 对应
            if (addGroupInfo(group))
            {   //查找 用户分组信息  ( 此时已经获得 group id )
                string sql = string.Format("select * from FFTX_Group_to_Friend where user_id='{0}' and group_id='{1}'", group.User_Id, group.Group_Id);

                SqlDataReader sqldr = SqlDB.ExecuteReader(sql);
                if (sqldr != null && sqldr.HasRows)
                {
                    sqldr.Read();
                    //获取 组人数
                    group.Group_Friend_Num = sqldr.GetInt32(4);
                    return true;
                }
                return false;
            }
            
            return false;
        }

        /// <summary>
        /// 根据User_Id获取 用户分组详情
        /// </summary>
        /// <param name="user">用户类</param>
        /// <returns>返回list 包含用户所有分组</returns>
        public List<Group> getGroupFriendInfo(User user)
        {
            
            string sql = string.Format("select * from FFTX_Group_to_Friend where user_id='{0}'",user.User_Id);
            SqlDataReader sqldr = SqlDB.ExecuteReader(sql);
            if (sqldr != null && sqldr.HasRows)
            {
                List<Group> list = new List<Group>();
                while (sqldr.Read())
                {
                    Group p = new Group();
                    p.User_Id = sqldr.GetValue(1) + "";
                    p.Group_Id = sqldr.GetInt32(2);
                    p.Group_Name = sqldr.GetValue(3) + "";
                    p.Group_Friend_Num = sqldr.GetInt32(4);

                    list.Add(p);

                }
                return list;
               
            }
            return null;
        }
        /// <summary>
        /// 用户组 人数加一
        /// </summary>
        /// <param name="f">需要User_id Group_id</param>
        /// <returns></returns>
        public bool plusOneInGroupNum(Friend f)
        {
            //用户所在组 数量+1
            string sql = string.Format("update FFTX_Group_to_Friend set group_friend_num = (group_friend_num+1) where user_id ='{0}',group_id={1}", f.User_Id, f.Group_Id);
            int result = SqlDB.ExecuteNonQuery(sql);
            if (result == 1)
                return true;
            else
                return false;
            
        }
        public bool decOneInGroupNum(Friend f)
        {
            //用户所在组 数量+1
            string sql = string.Format("update FFTX_Group_to_Friend set group_friend_num = (group_friend_num-1) where user_id ='{0}',group_id={1}", f.User_Id, f.Group_Id);
            int result = SqlDB.ExecuteNonQuery(sql);
            if (result == 1)
                return true;
            else
                return false;
        }

    
    }
       

}