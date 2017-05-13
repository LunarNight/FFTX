using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FFTX.Models;
using FFTX.ModelsSql;
namespace FFTX.Controllers
{
    public class FriendController : Controller
    {
        //
        // GET: /Friend/

        public ActionResult Index()
        {
            return View();
        }
        //添加朋友  关注
        public ActionResult addFriend(Friend f)
        {
            return View();
        }
        //删除朋友  取消关注
        public ActionResult delFriend(Friend f)
        {
            return View();
        }
        //改备注
        public ActionResult changeRemark(Friend f)
        {
            return View();
        }
        //改分组
        public ActionResult changeGroup(Friend f)
        {
            return View();
        }
    }
}
