using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FFTX.Controllers
{
    public class MailController : Controller
    {
        //
        // GET: /Mail/

        //站内信页
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult lookPhotoLike()
        {
            return View();
        }
        public ActionResult lookPhotoComment()
        {
            return View();
        }
        public ActionResult lookBoardMessage()
        {
            return View();
        }
        public ActionResult lookPhotoShare()
        {
            return View();  
        }
    }
}
