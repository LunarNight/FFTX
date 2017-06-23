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
        //搜索照片名 调用 PhotoSql searchName
        public ActionResult search()
        {
            //搜索传递
            string keywords = Request.Form["keywords"];
            //搜索后评论照片传递参数
            if (keywords == null)
            {
                keywords = Request.QueryString["keywords"];
            }
            //防止出错
            if(keywords==null){
                keywords = "空";
            }
            PhotoSql ps = new PhotoSql();
            List<Photo> plist = ps.searchName(keywords);
            ViewBag.searchResult = plist;
            ViewBag.keywords = keywords;
            return View();
        }
        //
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
