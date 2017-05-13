using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FFTX.Models;
using FFTX.ModelsSql;
namespace FFTX.Controllers
{
    public class PhotoShareController : Controller
    {
        //
        // GET: /PhotoShare/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult addShare(User u,Photo p)
        {
            return View();
        }
        public ActionResult deleteShare(User u, Photo p)
        {
            return View();
        }
    }
}
