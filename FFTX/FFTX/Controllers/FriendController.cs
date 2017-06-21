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
        //显示好友页面
        public ActionResult Index()
        {
            User u = new User();
            u.User_Id = ((User)Session["user"]).User_Id;
            FriendSql fsl = new FriendSql();
            List<Friend> flist =  fsl.getFriendList(u);
            ViewBag.flist = flist;
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
        //好友相册页面
        public ActionResult albumPage()
        {
            string uid = ((User)Session["user"]).User_Id;
            //获取好友个人信息
            string fid = Request.QueryString["friend_id"];
            User friend = new User();
            friend.User_Id = fid;
            UserSql usl = new UserSql();
            friend = usl.getUserInfo(friend);
            ViewBag.friend = friend;
            //获取好友相册
            AlbumSql asql = new AlbumSql();
            //获取好友相册
            Album album = new Album();
            try
            {
                album.User_Id = friend.User_Id;
            }
            catch (NullReferenceException)
            {
                return RedirectToAction("index", "Login");
            }
            List<Album> list = asql.getFriendAlbumInfo(album);
            ViewBag.list = list;
            return View();
        }
        //显示好友相片
        public ActionResult openAlbum()
        {
            Album al = new Album();
            AlbumSql asql = new AlbumSql();
            string id = Request.QueryString["album_id"];
            int page = Int32.Parse(Request.QueryString["page"]);
            al.Album_Id = Int32.Parse(id);
            //获取相册信息
            al = asql.getAlbumInfoById(al);
            //获取分页总页数
            int pageNum = asql.getPageNum(al);
            //传入相册id 和页数
            List<Photo> list = asql.getPhotosById(al, page);

            ViewBag.photo_list = list;
            ViewBag.pageNum = pageNum;
            ViewBag.album_id = al.Album_Id;
            ViewBag.album_name = al.Album_Name;
            return View(); 
        }
        //评论好友图片
        public ActionResult addComment(Comment c)
        {
            //获取用户名以及用户id
            c.Comment_Time = DateTime.Now;
            c.Comment_User_Id = ((User)Session["user"]).User_Id;
            c.Comment_Name = ((User)Session["user"]).User_Name;
            c.Photo_Id = Int32.Parse(Request.Form["photo_id"]);

            //c.Reply_Id = 0为相片   否则是 Comment_id
            c.Reply_Id = Int32.Parse(Request.Form["reply_id"]);
            c.Reply_User_Id = Request.Form["reply_user_id"];
            c.Reply_User_Name = Request.Form["reply_user_name"];
            //站内信标识
            c.Comment_Flag = 0;
            CommentSql csl = new CommentSql();
            bool l = csl.addComment(c);
            Photo p = new Photo();
            p.Photo_Id = c.Photo_Id;
            PhotoSql ps = new PhotoSql();
            ps.getPhotoInfo(p);
            int aid = p.album_id;
            //成功与否都返回此页面
            return RedirectToAction("openAlbum", "Friend", new { album_id = aid });

        }
    }
}
