using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FFTX.Controllers
{
    public class BoardMessageController : Controller
    {
        //
        // GET: /Board/



        //添加留言
        public ActionResult addMessage()
        {
            return View();
        }
        //回复留言
        public ActionResult replyMessage()
        {
            return View();
        }
        //删除留言
        public ActionResult deleteMessage()
        {
            return View();
        }

        //功能页面跳转
        public ActionResult Index()
        {
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
