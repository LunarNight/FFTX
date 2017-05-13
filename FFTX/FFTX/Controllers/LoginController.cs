using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FFTX.Models;
using FFTX.ModelsSql;
using System.Web.SessionState;

using System.IO;
namespace FFTX.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/

        public ActionResult Index()
        {   
            
            
            return View();
        }
        //登录 获取用户信息从数据库中验证
        public ActionResult Login(User user)
        {
            
            UserSql us = new UserSql();
            
            if (us.judgeUser(user))
            {
                User u = us.getUserInfo(user);
                //应该验证是否封号
                if (u.User_State == 0)
                {
                    //return 封号view;
                    return View("IdBlocked");
                }
             
                Session["LoginState"] = "已登录";
                //获取用户的所有信息.  存放session
                Session["user"] = (User)u;
                return RedirectToAction("Index", "FFTXIndex"); 
            }
            else
                return View("LoginFail");

        }

        //申请解封
        public ActionResult applyDeblock(UserRequest ur)
        {
            ur.Request_Time = DateTime.Now;
            UserSql us = new UserSql();
            if (us.requestDeblock(ur))
            {
                Response.Write("申请完成,请等待");
                Response.End();
            }
            else
            {
                Response.Write("申请失败,请重试");
                Response.End();
            }
            
            return View();
        }

/* 隐藏 找回密码  在findPWD中 有新的
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
 * */
        public ActionResult ConfirmInfo()
        {
            return View();
        }

        //测试上传
        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file)
        {
            
            if (file == null){
                ViewBag.Msg = "没有文件";
                return View();
            }else{
                ViewBag.Msg = "有文件";
                //根据用户名创建文件夹保存照片
                string user_id = "阿呆";
                string path = Request.MapPath("~/UserPhoto/" + user_id);

                if (Directory.Exists(path) == false)//如果不存在就创建file文件夹
                {
                    Directory.CreateDirectory(path);
                }
                file.SaveAs(path+"/"+file.FileName);
            }
                
            return View();
        }
    }
}
