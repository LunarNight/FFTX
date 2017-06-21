using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;
using FFTX.Models;
using FFTX.ModelsSql;
using System.IO;
namespace FFTX.Controllers
{
    public class ManageController : Controller
    {
        public ActionResult changeHeadImg(HttpPostedFileBase file)
        {
            if (file == null)
            {
                return RedirectToAction("Index", "Manage", new {headImgState = 0 });
            }
            else
            {
                //根据用户名创建文件夹保存照片
                string user_id = ((User)Session["user"]).User_Id;
                string path = Request.MapPath("~/Content/headimg/" );
                string hz = file.FileName.Substring(file.FileName.LastIndexOf("."));

                if (Directory.Exists(path) == false)//如果不存在就创建file文件夹
                {
                    Directory.CreateDirectory(path);
                }
                string fname = user_id + hz;
                file.SaveAs(path + fname);//保存文件
                //更新数据库
                User u = new User();
                u.User_Id = user_id;
                UserSql us = new UserSql();
                u = us.getUserInfo(u);
                u.User_HeadImg = "/Content/headimg/"+fname;
                us.changeHeadImg(u);
                //更新数据库

                return RedirectToAction("Index", "Manage", new { headImgState = 1 });
            }
        }
        //修改个人信息
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
                return RedirectToAction("Index", "Manage", new { infoState = 1});
            }
            else
            {
                return RedirectToAction("Index", "Manage", new { infoState = 0 });
            }

        }
        //修改密码
        public ActionResult changePassword(User user)
        {
            User u = (User)Session["user"];
            //验证密码是否相同
            if (u.User_Password.Equals(user.User_Password))
            {
                u.User_Password = user.Password_Answer;
                new UserSql().changePwd(u);
                return RedirectToAction("Index", "Manage", new { pwdState = 1 });
            }
            else
            {
                return RedirectToAction("Index", "Manage", new { pwdState = 0 });
            }

        }
        //修改信息页面主页
        public ActionResult Index()
        {
            //获取用户信息
            User u = (User)Session["user"];
            ViewBag.user = u;
            //获取修改密码状态 (弹窗)
            if (Request.QueryString["pwdState"]!=null)
            {
                int pwdState = Int32.Parse(Request.QueryString["pwdState"]);
                ViewBag.pwdState = pwdState;
            }
            //获取修改信息状态  (弹窗)
            if (Request.QueryString["infoState"]!=null)
            {
                int infoState = Int32.Parse(Request.QueryString["infoState"]);
                ViewBag.infoState = infoState;
            }
            //获取修改头像状态  (弹窗)
            if (Request.QueryString["headImgState"] != null)
            {
                int headImgState = Int32.Parse(Request.QueryString["headImgState"]);
                ViewBag.headImgState = headImgState; 
            }
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
