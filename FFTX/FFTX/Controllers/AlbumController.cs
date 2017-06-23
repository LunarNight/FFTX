using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;
using FFTX.Models;
using FFTX.ModelsSql;
namespace FFTX.Controllers
{
    public class AlbumController : Controller
    {
        //
        // GET: /Album/
        
        //我的相册页面  需要获取用户的所有相册
        public ActionResult Index()
        {
            ViewBag.info = "我就试试看";
            AlbumSql asql = new AlbumSql();
            Album album = new Album();
            try
            {
                album.User_Id = ((User)Session["user"]).User_Id;
            }
            catch (NullReferenceException)
            {
                return RedirectToAction("index", "Login");
            }
            List<Album> list = asql.getAlbumInfo(album);
            ViewBag.list = list;
           
            return View();
        }

        //获取相册id,获取id内的相片 是否需要验证用户id??

        //*****此方法应该移动到PhotoController中*******
        //打开相册
        public ActionResult openAlbum()
        {
            Album al = new Album();
            AlbumSql asql = new AlbumSql();
            string id = Request.QueryString["album_id"];
            int page = Int32.Parse(Request.QueryString["page"]);

            al.Album_Id = Int32.Parse(id);
            //获取相册信息
            al = asql.getAlbumInfoById(al);
            //验证是否是本人????

            //获取分页总页数
            int pageNum = asql.getPageNum(al);

            //传入相册id 和页数
            List<Photo> list = asql.getPhotosById(al,page);

            ViewBag.photo_list = list;
            ViewBag.pageNum = pageNum;
            ViewBag.album_id = al.Album_Id;
            ViewBag.album_name = al.Album_Name;
            ViewBag.thisPage = page;
            return View(); 
        }

        //跳转到 创建相册页面
        public ActionResult creatAlbum(Album album)
        {
            return View(); 
        }

        //是否创建成功 成功返回首页  不成功的话 还没做
        public ActionResult isCreatSuccess(Album album)
        {
            //创建成功  返回 我的相册主页
            //创建失败  跳转到失败页面吧. 好像不会失败
            album.User_Id = ((User)Session["user"]).User_Id;
            AlbumSql asql = new AlbumSql();
            asql.addAlbum(album);
            return RedirectToAction("Index"); 
        }

        //删除相册
        public ActionResult deleteAlbum()
        {
            //删除相册内所有图片
            string path = Request.MapPath("~");
            Album album = new Album();
            AlbumSql asl = new AlbumSql();
            album.Album_Id = Int32.Parse(Request.Form["delete_album_id"]);
            asl.deleteAlbum(album, path);
            //删除相册
            return RedirectToAction("Index","Album");
        }
        public ActionResult changeCover()
        {
            Album album = new Album();
            string imgsrc = Request.Form["imgsrc"];
            album.Album_Cover = imgsrc;
            int aid = Int32.Parse(Request.Form["album_id"]);
            album.Album_Id = aid;
            AlbumSql asl = new AlbumSql();
            asl.changeAlbumCover(album);
            return RedirectToAction("openAlbum", new { album_id = aid, page = 1 });
        }
        //重命名相册
        public ActionResult renameAlbum(Album album)
        {
            int aid = Int32.Parse(Request.Form["album_id"]);
            album.Album_Id = aid;
            AlbumSql asl = new AlbumSql();
            if (asl.renameAlbum(album))
            {
                return RedirectToAction("openAlbum", new { album_id = aid, page = 1 });
            }
            else
            {
                return Content("重命名失败");
            }
        }

        //功能页面跳转
        public ActionResult Board()
        {
            return RedirectToAction("index", "Board");
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
