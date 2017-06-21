using System;
using System.IO;
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
            Photo p = new Photo();
            string id = Request.QueryString["photo_id"];
            if(id!=null)
                p.Photo_Id = Int32.Parse(id);
            PhotoSql ps = new PhotoSql();
            //根据 PHOTO ID 获取信息
            ps.getPhotoInfo(p);
            //扔到前台  照片和用户信息
            ViewBag.photo = p;
            ViewBag.user = (User)Session["user"];

            //获取此照片所有评论
            CommentSql csl = new CommentSql();
            Comment c = new Comment();
            List<Comment> comment_list = csl.getComments(p.Photo_Id);
            ViewBag.comment_list = comment_list;
            return View();
        }
        //上传
        public ActionResult uploadPhoto(HttpPostedFileBase file)
        {
            //相册id
            int aid = Int32.Parse(Request.Form["album_id"]);
            int label = Int32.Parse(Request.Form["imgLabel"]);

            if (file == null)
            {
                return RedirectToAction("openAlbum", "Album", new { album_id = aid });
            }
            else
            {
                //根据用户名创建文件夹保存照片
                string user_id = ((User)Session["user"]).User_Id;
                string path = Request.MapPath("~/Content/photo/" + user_id);
                
                //获取总的秒数
                DateTime oldTime=new DateTime(1970,1,1);
                TimeSpan span=DateTime.Now.Subtract(oldTime);
                double ms = span.TotalMilliseconds;
                String time = ms.ToString().Split('.')[0];


                if (Directory.Exists(path) == false)//如果不存在就创建file文件夹
                {
                    Directory.CreateDirectory(path);
                }
                file.SaveAs(path + "/" + time + "(" + file.FileName);//保存文件

                //在数据库中添加一条数据
                PhotoSql ps = new PhotoSql();
                Photo p = new Photo();
                p.Photo_Src = "/Content/photo/" + user_id + "/" + time + "(" + file.FileName;
                p.Photo_Time = DateTime.Now;
                p.Photo_Label = label;//照片标签选取
                p.album_id = aid; //照片所在相册
                ps.uploadPhoto(p);

                return RedirectToAction("openAlbum", "Album", new { album_id = aid,page=1});
            }
           
        }
        //重命名
        public ActionResult renamePhoto()
        {
            Photo p = new Photo();
            PhotoSql ps = new PhotoSql();
            int aid = Int32.Parse(Request.Form["album_id"]);
            string name = Request.Form["rename_img"];
            int pid = Int32.Parse(Request.Form["rename_img_id"]);
            p.Photo_Id = pid;
            bool result =ps.getPhotoInfo(p);
            if (!result)
                return Content("命名失败");

            string oldsrc = p.Photo_Src;
            //Content/xx/12321424( 
            string front = oldsrc.Substring(0, oldsrc.LastIndexOf("(") + 1);
            //.png
            string end = oldsrc.Substring(oldsrc.LastIndexOf("."));
            //新的路径 (文件名改变) 保存到数据库。。
            string newsrc = front + name + end;
            /*   好像用不到这俩东西啊。。。*/
            //原来文件名
            string filename = oldsrc.Substring(oldsrc.LastIndexOf("/"));
            //新的文件名
            string newname = filename.Substring(0,filename.IndexOf("(")+1)+name+end;

            string path = Request.MapPath("~");

            //改变文件名
            System.IO.File.Move(path+oldsrc,path+newsrc);
            //update 数据库
            p.Photo_Src = newsrc;

            ps.renamePhoto(p);;
            bool r = true;
            if (r)
                return RedirectToAction("openAlbum", "Album", new { album_id = aid, page = 1 });
            else
                return Content("删除失败");
        }
        //删除
        public ActionResult deletePhoto()
        {
            int aid = Int32.Parse(Request.Form["album_id"]);
            int pid = Int32.Parse(Request.Form["delete_img_id"]);

            Photo p = new Photo();
            PhotoSql ps = new PhotoSql();
            p.Photo_Id = pid;
            //获取照片信息
            ps.getPhotoInfo(p);
            //删除 有关此照片的所有信息(分享 评论 点赞)
            bool result = ps.deletePhoto(p);
            if (result){
                //把原图片文件删除
                string path = Request.MapPath("~");
                //获取项目运行路径
                FileInfo file = new FileInfo(path+p.Photo_Src);
                if (file.Exists)
                {
                    file.Delete();
                }


                return RedirectToAction("openAlbum", "Album", new { album_id = aid, page = 1 });
            }
                
            else
                return Content("删除失败");
            
        }
        //处理图片
        public ActionResult dealPhoto()
        {
            Photo p = new Photo();
            string id = Request.QueryString["photo_id"];
            if (id != null)
                p.Photo_Id = Int32.Parse(id);
            PhotoSql ps = new PhotoSql();
            //根据 PHOTO ID 获取信息
            ps.getPhotoInfo(p);
            //扔到前台  照片和用户信息
            ViewBag.photo = p;
            ViewBag.user = (User)Session["user"];
            //修改图片文件
            return View();
        }
        // ajax实现 保存图片
        public ActionResult saveDealPhoto(string imgData, string id)
        {

            PhotoSql ps = new PhotoSql();
            Photo p = new Photo();
            p.Photo_Id = Int32.Parse(id);
            ps.getPhotoInfo(p);
            string path = Request.MapPath("~");
            try
            {
                FileStream fs = System.IO.File.Create(path+p.Photo_Src);
                byte[] bytes = Convert.FromBase64String(imgData);
                fs.Write(bytes, 0, bytes.Length);
                fs.Close();
            }
            catch (Exception ex)
            {
            }

            return Content("修改成功,请清除缓存或重命名");
        }
        //评论照片
        public ActionResult commentPhoto()
        {
            Photo p = new Photo();
            string id = Request.QueryString["photo_id"];
            if (id != null)
                p.Photo_Id = Int32.Parse(id);
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

    }
}
