using System;
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
                return View("LoginFail");
            }
            else
            {
                ViewBag.pwd = pwd;
                return View("ShowPwd");
            }

        }
        public ActionResult ConfirmInfo()
        {
            return View();
        }
    }
}
