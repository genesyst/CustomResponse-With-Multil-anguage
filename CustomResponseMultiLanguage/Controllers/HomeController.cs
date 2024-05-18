using CustomResponseMultiLanguage.serviceLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace CustomResponseMultiLanguage.Controllers
{
    [CustomResponseView]
    public class HomeController : Controller
    {
        //[CustomActionView]
        public ActionResult Index()
        {
            ViewBag.Language = GetCurrentLanguage();
            ViewBag.Name = "by Keatkamon Thongsin";

            return View();
        }

        [HttpPost]
        public ActionResult SetLanguage(string language)
        {
            HttpCookie cookie = new HttpCookie("SelectedLanguage", language);
            cookie.Expires = DateTime.Now.AddYears(1);
            Response.Cookies.Add(cookie);

            return Redirect(Request.UrlReferrer.ToString());
        }

        private string GetCurrentLanguage()
        {
            HttpCookie cookie = Request.Cookies["SelectedLanguage"];
            string culture_name = "en-US";
            string msg = "";
            if (cookie == null)
            {
                msg = "ภาษาท้องถิ่นของท่านคือ ";
                culture_name = Thread.CurrentThread.CurrentUICulture.Name;
            }
            else
            {
                msg = "ภาษาที่ท่านเลือกคือ ";
                culture_name = cookie.Value;
            }

            switch (culture_name)
            {
                case "en-US":
                    msg += "ภาษาอังกฤษ"; break;
                case "th-TH":
                    msg += "ภาษาไทย"; break;
                case "ch-CH":
                    msg += "ภาษาจีน"; break;
                case "lo-LA":
                    msg += "ภาษาลาว"; break;
            }

            return msg;

        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}