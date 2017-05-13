using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FFTX.Models;
using FFTX.ModelsSql;
namespace FFTX.Controllers
{
    public class PhotoCommentController : Controller
    {
        //
        // GET: /PhotoComment/

        public ActionResult Index()
        {
            return View();
        }
        //添加评论
        public ActionResult addComment(Comment c)
        {
            return View();
        }
        //删除评论
        public ActionResult deleteComment(Comment c)
        {
            return View();
        }
    }
}
