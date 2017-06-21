using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FFTX.Models;
using FFTX.ModelsSql;
namespace FFTX.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/

        //主页
        public ActionResult Index(Admin admin)
        {
            
            if("已登录".Equals(Session["LoginState"])){
                //获取解封请求
                AdminSql ads = new AdminSql();
                List<UserRequest> list = ads.getUserRequst();
                if(list==null){
                    ViewBag.listinfo = "没有用户请求";
                }else{
                    ViewBag.listinfo = "有以下用户请求解封";
                    ViewBag.user_request_list = list;
                }
                return View();

            }else{
                return View("LoginFail");
            }
        }
        
        //验证登录
        public ActionResult confirmLogin(Admin admin)
        {
            AdminSql ads = new AdminSql();
            //验证 ID 和密码
            if (ads.Login(admin))
            {
                //验证信息成功
                //成功设置session
                Session["LoginState"] = "已登录";
                Session["Admin"] = admin;
                ViewBag.se = Session["LoginState"].ToString();
                ViewBag.name = admin.Admin_name;

                return RedirectToAction("index");
            }
            else
            {
                //验证失败
                //或许使用ajax
                //Response.Write("AAA");
                //Response.End();
                return View("LoginFail");
            }
        }
        public ActionResult Login()
        {
            return View();
        }

        //页面跳转
        public ActionResult blockUserPage()
        {
            return View();
        }
        //封禁用户
        public ActionResult blockUser(AdminOperate ao)
        {
            ao.Admin_Id = ((Admin)Session["Admin"]).Admin_Id;
            ao.Operate_Time = DateTime.Now;
            AdminSql ads = new AdminSql();
            if (ads.blockUser(ao))
            {
                //封号成功
            }
            ///
            return RedirectToAction("index");
        }

        //处理请求(解封)
        public ActionResult handleRequest()
        {

           //获取 处理状态
            string uid = Request.QueryString["user_id"];
            string state = Request.QueryString["state"];
            UserRequest ur = new UserRequest();
            ur.User_Id = uid;
            //根据 state进行处理   0表示解封  1是拒绝
            UserRequestSql urs = new UserRequestSql();
            if ("0".Equals(state))
            {
                //解封   把数据删除
                urs.deblockUser(ur);        
                //将 user state 设置成1
                new UserSql().changeUserState(ur.User_Id, 1);
            }
            else
            {
                //拒绝解封  设置成1
                urs.blockUser(ur);
                //将 user state 设置成2   不允许申请
                new UserSql().changeUserState(ur.User_Id, 2);
            }
            return RedirectToAction("index");
        }
        public ActionResult searchReason()
        {
            string uid = Request.QueryString["user_id"];
            //获取被封用户详细信息
            User u = new User();
            u.User_Id = uid;
            UserSql us = new UserSql();
            u = us.getUserInfo(u);
            ViewBag.blockUser = u;

            //获取封号原因
            AdminOperate ao = new AdminOperate();
            AdminSql asl = new AdminSql();
            ao = asl.getAdminOperateByuid(u);
            ViewBag.operate = ao;

            return View();
        }

    }
}
