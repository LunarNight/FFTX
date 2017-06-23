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
    public class BoardMessageController : Controller
    {
        //
        // GET: /Board/



        //添加留言
        public ActionResult addMessage(Message m)
        {
            User u = (User)Session["user"];
            m.Message_User_Id = u.User_Id;
            m.Message_User_Name = u.User_Name;
            m.Message_Time = DateTime.Today;
            m.Message_Friend_Id = Request.Form["Message_Friend_Id"];
            m.Message_Reply_Id = Int32.Parse(Request.Form["Message_Reply_Id"]);
            MessageSql ms = new MessageSql();
            ms.addMessage(m);
            return RedirectToAction("Index","BoardMessage");
        }
        //回复留言
        public ActionResult replyMessage()
        {
            return View();
        }
        //删除留言
        public ActionResult deleteMessage()
        {
            int id = Int32.Parse(Request.QueryString["message_id"]);
            MessageSql ms = new MessageSql();
            ms.deleteMessage(id);
            return RedirectToAction("Index", "BoardMessage");
        }

        //功能页面跳转
        public ActionResult Index()
        {
            //获取用户所有的留言
            User u = new User();
            u.User_Id = ((User)Session["user"]).User_Id;
            int page;
            //获取当前页数
            if (Request.QueryString["page"] == null)
                page = 1;
            else
                page = Int32.Parse(Request.QueryString["page"]);
            MessageSql ms = new MessageSql();
            int pageNum = ms.getPageNum(u);
            ViewBag.pageNum = pageNum;
            //获取留言条数
            List<Message> list =  ms.getMessage(u, page);
            ViewBag.message_list = list;
            ViewBag.thisPage = page;

            return View();
        }
        public ActionResult Album()
        {
            return RedirectToAction("index", "Album");
        }
        public ActionResult FFTXIndex()
        {
            return RedirectToAction("index", "FFTXIndex");
        }
        public ActionResult Manage()
        {
            return RedirectToAction("index", "Manage");
        }
    }
}
