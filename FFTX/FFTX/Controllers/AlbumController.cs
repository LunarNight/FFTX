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
            album.User_Id = ((User)Session["user"]).User_Id;
            List<Album> list = asql.getAlbumInfo(album);
            ViewBag.list = list;
           
            return View();
        }

        //获取相册id,获取id内的相片 是否需要验证用户id??

        //*****此方法应该移动到PhotoController中*******
        public ActionResult openAlbum()
        {
            Album al = new Album();
            string id = Request.QueryString["album_id"];
            al.Album_Id = Int32.Parse(id);
            al.User_Id = ((User)Session["user"]).User_Id;

            //ViewBag.photo_list = ***;
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
        public ActionResult deleteAlbum(Album album)
        {
            return View();
        }

        //重命名相册
        public ActionResult renameAlbum(Album album)
        {
            return View();
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
