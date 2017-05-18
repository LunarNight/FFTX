using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FFTX.Models;
using FFTX.ModelsSql;
using System.IO;
namespace FFTX.Controllers
{
    public class PhotoController : Controller
    {
        //
        // GET: /Photo/

        //打开相册的页面  需要获取此相册内所有照片
        public ActionResult Index()
        {
            Photo p = new Photo();
            string id = Request.QueryString["photo_id"];
            if(id!=null)
                p.Photo_Id = Int32.Parse(id);
            PhotoSql ps = new PhotoSql();
            //根据 PHOTO ID 获取信息
            ps.getPhotoInfo(p);
            //扔到前台
            ViewBag.photo = p;
            return View();
        }
        //上传
        public ActionResult uploadPhoto(HttpPostedFileBase file)
        {
            if (file == null)
            {
                ViewBag.Msg = "没有文件";
                return View();
            }
            else
            {
                ViewBag.Msg = "有文件";
                //根据用户名创建文件夹保存照片
                string user_id = "阿呆";
                string path = Request.MapPath("~/UserPhoto/" + user_id);

                if (Directory.Exists(path) == false)//如果不存在就创建file文件夹
                {
                    Directory.CreateDirectory(path);
                }
                file.SaveAs(path + "/" + file.FileName);

                //在数据库中添加一条数据
            }

            return View();
        }
        //重命名
        public ActionResult renamePhoto()
        {
            Photo p = new Photo();
            string pid = Request.QueryString["photo_id"];
            p.Photo_Id = Int32.Parse(pid);

            PhotoSql ps = new PhotoSql();
            //重命名 修改数据库中的字段

            //修改文件的文件名
            bool result = ps.renamePhoto(p);
            if (result)
                return RedirectToAction("Index");
            else
                return Content("删除失败");
        }
        //删除
        public ActionResult deletePhoto()
        {
            Photo p = new Photo();
            string pid = Request.QueryString["photo_id"];
            p.Photo_Id = Int32.Parse(pid);
            string aid = Request.QueryString["album_id"];
            PhotoSql ps = new PhotoSql();
            //获取照片信息
            ps.getPhotoInfo(p);
            //删除 photo数据库中信息
            bool result = ps.deletePhoto(p);

            //删除 点赞表中的信息
            //code..
            
            //删除 图片分享 ?? 删不删呢.. 弄一张 照片已删除的照片 可以不删除分享.

            //删除 图片评论  同上

            if (result){
                //把原图片文件删除
                string filePath = "";

                int k = p.Photo_Src.LastIndexOf("/");
                string fileName = p.Photo_Src.Substring(k+1);

                
                return RedirectToAction("openAlbum", "Album", new { album_id = aid });
            }
                
            else
                return Content("删除失败");
            
        }
        //裁剪
        public ActionResult cutPhoto(Photo p)
        {
            //修改图片文件
            return View();
        }
        //滤镜
        public ActionResult addFilterPhoto(Photo p)
        {
            //修改图片文件
            return View();
        }
        //水印 
        public ActionResult waterMarkPhoto(Photo p)
        {
            //修改图片文件
            return View();
        }
    }
}
