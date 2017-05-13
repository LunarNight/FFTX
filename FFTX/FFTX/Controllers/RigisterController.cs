using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FFTX.Models;
using FFTX.ModelsSql;
using System.Web.SessionState;
namespace FFTX.Controllers
{
    public class RigisterController : Controller
    {
        //
        // GET: /Rigister/

        public ActionResult Index()
        {
            return View();
        }
        //注册事件
        public ActionResult Register(User user)
        {

           UserSql us = new UserSql();
           int regist_info= us.addUser(user);
           if (regist_info == 1)
           {
               //注册成功  创建 我的好友分组
               Group p = new Group();
               p.User_Id = user.User_Id;
               p.Group_Name = "我的好友";
               new GroupSql().addGroupFriendInfo(p);

               return RedirectToAction("Index", "Login"); ; 
           }
           else
           {
               Response.Write("注册失败了注册失败了注册失败了");
               return View("index");
           }
          
        }

    }
}
