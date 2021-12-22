using Odev.Models.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Odev.Controllers
{
    public class HomeController : Controller
    {
        private AraçKiralamaDBContext db = new AraçKiralamaDBContext();
        // GET: Home
        public ActionResult Index()
        {
            ViewBag.Urunler = db.Urun.ToList().OrderByDescending(x => x.UrunId);

            ViewBag.Iletisim = db.Iletisim.SingleOrDefault();

            ViewBag.Blog = db.Blog.ToList().OrderByDescending(x => x.BlogId);

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
    }
}