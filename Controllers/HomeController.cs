using Odev.Models.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Text.RegularExpressions;

namespace Odev.Controllers
{
    public class HomeController : Controller
    {
        private AraçKiralamaDBContext db = new AraçKiralamaDBContext();
        // GET: Home
        public ActionResult Index()
        {
            ViewBag.Urunler = db.Urun.ToList().OrderByDescending(x => x.UrunId);

            return View();
        }

        public ActionResult SliderPartial()
        {
            return View(db.Slider.ToList().OrderByDescending(x=>x.SliderId));

        }
        
        public ActionResult UrunPartial()
        {
            return View(db.Urun.ToList());
        }

        public ActionResult Hakkimizda()
        {
            

            return View(db.Hakkimizda.SingleOrDefault());
        }

        public ActionResult Araclarimiz()
        {
            return View(db.Urun.ToList().OrderByDescending(x=>x.UrunId));
        }

        public ActionResult Iletisim()
        {
            return View(db.Iletisim.SingleOrDefault());
        }

        [HttpPost]
        public ActionResult Iletisim(string adsoyad = null, string email = null, string konu = null, string mesaj = null)
        {
            if (adsoyad != null && email != null)
            {
                WebMail.SmtpServer = "smtp.gmail.com";
                WebMail.EnableSsl = true;
                WebMail.UserName = "odev.mail54@gmail.com";
                WebMail.Password = "ysfemre12";
                WebMail.SmtpPort = 587;
                WebMail.Send("odev.mail54@gmail.com", konu, email, mesaj);
                ViewBag.Uyari = "Mesajınız Başarıyla Gönderildi.";
                
            }
            else
            {
                ViewBag.Uyari = "Hata Oluştu! Tekrar Deneyin.";
            }
            return View();
        }

        public ActionResult Blog()
        {
            return View(db.Blog.Include("Kategori").ToList().OrderByDescending(x=>x.BlogId));
        }

        public ActionResult BlogDetay(int id)
        {
            var b = db.Blog.Include("Kategori").Where(x => x.BlogId == id).SingleOrDefault();
            return View(b);
        }

        public ActionResult BlogKategoriPartial()
        {
            return PartialView(db.Kategori.Include("Blogs").ToList().OrderBy(x => x.KategoriAd));
        }

        public ActionResult BlogKayitPartial()
        {
            return PartialView(db.Blog.ToList().OrderByDescending(x=>x.BlogId));
        }

        public ActionResult FooterPartial()
        {
            ViewBag.Urunler = db.Urun.ToList().OrderByDescending(x => x.UrunId);

            ViewBag.Iletisim = db.Iletisim.SingleOrDefault();

            ViewBag.Blog = db.Blog.ToList().OrderByDescending(x => x.BlogId);

            return PartialView();   
        }



    }
}