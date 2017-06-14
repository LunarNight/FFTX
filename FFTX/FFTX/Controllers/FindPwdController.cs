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
    public class FindPwdController : Controller
    {
        //
        // GET: /FindPwd/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult forgetPassword(User user)
        {

           
            UserSql us = new UserSql();
            //成功返回页面
            string pwd = us.forgetPassword(user);
            if (pwd == null)
            {
                //找回密码失败
                return Content("信息验证失败,请重新输入");
            }
            else
            {
                ViewBag.pwd = pwd;
                return Content("您的密码是:"+pwd);
            }

        }
        public ActionResult ConfirmInfo()
        {
            return View();
        }
        public ActionResult Test(string imgData)
        {
            string name ="dian";
            int k = imgData.Length;

            String savePath = Server.MapPath("/Content");
            string imgData2 = imgData.Replace("999aaa999", "+");
            int k1 = imgData2.Length;
            try
            {
                FileStream fs = System.IO.File.Create(savePath + "/" + name + ".jpg");
                //char[] i = imgData2.ToCharArray();
                //byte[] bytes = Convert.FromBase64CharArray(i,0,i.Length); 
                byte[] bytes = Convert.FromBase64String(imgData2);
                fs.Write(bytes, 0, bytes.Length);
                fs.Close();
            }
            catch (Exception ex)
            {
            }  


            return Content("修改成功" );
        }
    }
}
