using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FFTX.Models;
using FFTX.ModelsSql;
namespace FFTX.Controllers
{
    public class OtherFunctionController : Controller
    {
        //
        // GET: /OtherFunction/

        public ActionResult Index()
        {
            return View();
        }
        //排行榜
        public List<Photo> getChart()
        {
            return null;
        }
        //搜索 
        public ActionResult search(string keywords)
        {
            searchUser(keywords);
            searchPhoto(keywords);
            return View();
        }
        public List<User> searchUser(string user_name)
        {
            return null;
        }
        public List<Photo> searchPhoto(string photo_keywords)
        {
            return null;
        }
    }
}
