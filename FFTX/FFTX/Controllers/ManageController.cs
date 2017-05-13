using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;
using FFTX.Models;
using FFTX.ModelsSql;
namespace FFTX.Controllers
{
    public class ManageController : Controller
    {
        //
        // GET: /Manage/
        //修改密码
        public ActionResult changeInfo(User user)
        {
            //获取原本信息
            User cuser = (User)Session["user"];

            //用新的信息覆盖
            cuser.User_Name = user.User_Name;
            cuser.User_Sex = user.User_Sex;
            cuser.User_Phone = user.User_Phone;
            cuser.User_Email = user.User_Email;
            //进行信息修改
            if (new UserSql().updateUserInfo(cuser))
            {
                Session["user"] = new UserSql().getUserInfo(cuser);
            }
            else
            {
                Response.Write("修改失败！");
                Response.End();
            }

            return View();
        }
        public ActionResult changePassword(User user)
        {
            User u = (User)Session["user"];
            //验证密码是否相同
            if (u.User_Password.Equals(user.User_Password))
            {
                u.User_Password = user.Password_Answer;
                new UserSql().changePwd(u);
                Response.Write("修改成功！");
            }
            else
            {
                Response.Write("修改失败！");
                Response.End();
            }
            return View();
        }
        public ActionResult Index()
        {
            //获取用户信息
            User u = (User)Session["user"];
            ViewBag.user = u;

            return View();
        }
        public ActionResult Album()
        {
            return RedirectToAction("index", "Album");
        }
        public ActionResult Board()
        {
            return RedirectToAction("index", "Board");
        }
        public ActionResult FFTXIndex()
        {
            return RedirectToAction("index", "FFTXIndex");
        }
        
    }
}
