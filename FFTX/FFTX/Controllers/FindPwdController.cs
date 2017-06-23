using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FFTX.Models;
using FFTX.ModelsSql;
namespace FFTX.Controllers
{
    public class FindPwdController : Controller
    {
        //
        // GET: /FindPwd/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult forgetPassword(User user)
        {

           
            UserSql us = new UserSql();
            //成功返回页面
            string pwd = us.forgetPassword(user);
            if (pwd == null)
            {
                //找回密码失败
                return Content("信息验证失败,请重新输入");
            }
            else
            {
                ViewBag.pwd = pwd;
                return Content("您的密码是:"+pwd);
            }

        }
        public ActionResult ConfirmInfo()
        {
            return View();
        }
        public ActionResult Test(string id,string question,string answer)
        {
            User user = new User();
            user.User_Id = id;
            user.Password_Question = question;
            user.Password_Answer = answer;
            UserSql us = new UserSql();
            //成功返回页面
            string pwd = us.forgetPassword(user);
            if (pwd == null)
            {
                //找回密码失败
                return Content("信息验证失败,请重新输入");
            }
            else
            {
                ViewBag.pwd = pwd;
                return Content("您的密码是:" + pwd);
            }
        }
    }
}
