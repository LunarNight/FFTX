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
            //站内信数量修改
            new MailSql().addCommentMail(c.Reply_User_Id);
            Photo p = new Photo();
            p.Photo_Id = c.Photo_Id;
            PhotoSql ps = new PhotoSql();
            ps.getPhotoInfo(p);
            int aid = p.album_id;

            //判断从哪个页面跳转过来
            string from = Request.Form["from"];
            if (from != null && from.Equals("search"))
            {
                string kwd = Request.Form["keywords"];
                return RedirectToAction("search", "OtherFunction", new { keywords = kwd });
            }
            else if (from != null && from.Equals("FFTX"))
            {
                return RedirectToAction("Index", "FFTXIndex");
            }
            //跳转回相册
            return RedirectToAction("openAlbum", "Album", new { album_id = aid, page = 1 });

        }
       
    }
}
