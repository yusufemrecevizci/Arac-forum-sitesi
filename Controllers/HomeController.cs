using Odev.Models.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Text.RegularExpressions;
using PagedList;
using PagedList.Mvc;
using Odev.Models.Model;

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

        public ActionResult Blog(int Sayfa = 1)
        {
            return View(db.Blog.Include("Kategori").OrderByDescending(x=>x.BlogId).ToPagedList(Sayfa,4));
        }

        public ActionResult KategoriBlog(int id, int Sayfa = 1)
        {
            var b = db.Blog.Include("Kategori").OrderByDescending(x=>x.BlogId).Where(x => x.Kategori.KategoriId == id).ToPagedList(Sayfa, 5);
            return View(b);
        }

        public ActionResult BlogDetay(int id)
        {
            var b = db.Blog.Include("Kategori").Include("Yorums").Where(x => x.BlogId == id).SingleOrDefault();
            return View(b);
        }

        public JsonResult YorumYap(string adsoyad, string eposta, string icerik, int blogid) 
        {
            if (icerik == null)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            db.Yorum.Add(new Yorum { AdSoyad = adsoyad, Eposta = eposta, Icerik = icerik, BlogId = blogid, Onay = false });
            db.SaveChanges();
            
            return Json(false, JsonRequestBehavior.AllowGet);
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