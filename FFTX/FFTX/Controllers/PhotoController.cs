using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FFTX.Models;
using FFTX.ModelsSql;
namespace FFTX.Controllers
{
    public class PhotoController : Controller
    {
        //
        // GET: /Photo/

        //打开相册的页面  需要获取此相册内所有照片
        public ActionResult Index()
        {
            return View();
        }
        //上传
        public ActionResult uploadPhoto(Photo p)
        {
            return View();
        }
        //重命名
        public ActionResult renamePhoto(Photo p)
        {
            return View();
        }
        //删除
        public ActionResult deletePhoto(Photo p)
        {
            return View();
        }
        //裁剪
        public ActionResult cutPhoto(Photo p)
        {
            return View();
        }
        //滤镜
        public ActionResult addFilterPhoto(Photo p)
        {
            return View();
        }
        //水印 
        public ActionResult waterMarkPhoto(Photo p)
        {
            return View();
        }
    }
}
