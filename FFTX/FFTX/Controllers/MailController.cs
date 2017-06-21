using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FFTX.Models;
using FFTX.ModelsSql;
namespace FFTX.Controllers
{
    public class MailController : Controller
    {
        //
        // GET: /Mail/

        //站内信页
        public ActionResult Index()
        {
            User user = new User();
            user.User_Id = ((User)Session["user"]).User_Id;
            //获取站内信相关信息
            Mail m = new Mail();
            MailSql msl = new MailSql();
            m.User_Id = user.User_Id;
            msl.getMailInfo(m);
            ViewBag.mail = m;

            //获取评论信息
            if (m.Comment_Num != 0)
            {
                List<Comment> clist = new CommentSql().getMailComments(m.User_Id);
                ViewBag.comment_list = clist;
            }
            else
            {
                ViewBag.CommentList = null;
            }
            //获取分享信息
            if (m.Share_Num != 0)
            {
                ViewBag.ShareList = null;
            }
            else
            {
                ViewBag.ShareList = null;
            }
            //获取点赞信息
            if (m.Like_Num != 0)
            {
                ViewBag.LikeList = null;
            }
            else
            {
                ViewBag.LikeList = null;
            }
            return View();
        }
        public ActionResult lookPhotoLike()
        {
            return View();
        }

        //删除某一条站内信
        public ActionResult lookPhotoComment()
        {
            int cid = Int32.Parse(Request.QueryString["comment_id"]);
            //获取查看评论的id 将flag=1
            Comment c = new Comment();
            c.Comment_Id = cid;

            //获取站内信信息
            Mail m = new Mail();
            m.User_Id = ((User)Session["user"]).User_Id;
            MailSql msl = new MailSql();
            msl.getMailInfo(m);
            //改变未查看评论数量
            m.Comment_Num = m.Comment_Num - 1;

            //改变站内信数量 改变 评论查看位
            msl.lookComment(m,c);
            return RedirectToAction("Index");
        }

        //转到评论页面  传入 Comment Id
        public ActionResult commentPhoto()
        {
            int cid = Int32.Parse(Request.QueryString["comment_id"]);
            //根据 cid 获取 photo
            CommentSql cs = new CommentSql();
            Photo p = new Photo();
            p = cs.getPhotoInfoByCommentId(cid);

            PhotoSql ps = new PhotoSql();

            //根据 PHOTO ID 获取信息
            ps.getPhotoInfo(p);
            ViewBag.photo = p;
            ViewBag.user = (User)Session["user"];

            //获取上传此照片用户的信息
            User p_own = ps.getUserByPhoto(p);
            ViewBag.p_own = p_own;
            p.Photo_Src.Equals("");
            //获取此照片所有评论
            CommentSql csl = new CommentSql();
            Comment c = new Comment();
            List<Comment> comment_list = csl.getComments(p.Photo_Id);
            ViewBag.comment_list = comment_list;


            return View();
        }
        
        //添加评论
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
            //成功与否都返回此页面
            return RedirectToAction("Index", "Mail");

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
