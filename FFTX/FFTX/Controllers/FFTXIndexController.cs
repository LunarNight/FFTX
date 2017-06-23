using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FFTX.Models;
using FFTX.ModelsSql;
using System.Web.SessionState;
namespace FFTX.Controllers
{
    public class FFTXIndexController : Controller
    {
        //
        // GET: /Index/

        //验证是否登录  登录的话显示主页 否则返回登录页面
        public ActionResult Index(User user)
        {
            
            if (Session["LoginState"] != null){
            
                user.User_Id = ((User)Session["user"]).User_Id;
                ViewBag.user =(User) Session["user"];
               
                GroupSql gsql = new GroupSql();
                //获取好友分组信息
                List<Group> group_list = gsql.getGroupFriendInfo(user);
                ViewBag.group_list = group_list;
                //code

                //获取站内信信息 扔到ViewBag
                MailSql msl = new MailSql();
                Mail m = new Mail();
                m.User_Id = user.User_Id;
                msl.getMailInfo(m);
                ViewBag.mail = m;
                PhotoSql ps = new PhotoSql();
                //获取排行榜图片
                List<Photo> rankList =ps.getRankList();
                ViewBag.rankList = rankList;
                //获取推荐照片
                List<Photo> recommandList = ps.getRecommandList(user);
                ViewBag.recommandList = recommandList;
                return View();
            }
            else
                return RedirectToAction("index","Login");
            
        }

        public ActionResult searchFriend(User user)
        {
            FriendSql fsql = new FriendSql();
            ViewBag.friend = fsql.getFriendInfo(user);
            return View();
        }


        //创建分组
        public ActionResult addGroup(Group group)
        {
            group.User_Id = ((User)Session["user"]).User_Id;
            GroupSql gsql = new GroupSql();
            gsql.addGroupFriendInfo(group);
            return RedirectToAction("index", "FFTXIndex");
        }
        //分组页面 跳转用
        public ActionResult creatGroup()
        {
            return View();
        }
        public ActionResult Friend()
        {
            return RedirectToAction("index", "Friend");
        }
        public ActionResult Board()
        {
            return RedirectToAction("index", "BoardMessage");
        }
        public ActionResult Album()
        {
            return RedirectToAction("index","Album");
        }
        public ActionResult Manage()
        {
            return RedirectToAction("index", "Manage");
        }

    }
}
