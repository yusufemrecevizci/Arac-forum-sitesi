using Odev.Models.DataContext;
using Odev.Models.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Odev.Controllers
{
    public class UrunController : Controller
    {
        private AraçKiralamaDBContext db = new AraçKiralamaDBContext();
        // GET: Urun
        public ActionResult Index()
        {
            return View(db.Urun.ToList());
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Urun urun, HttpPostedFileBase ResimURL)
        {
            if (ModelState.IsValid)
            {
                if (ResimURL != null)
                {
                    WebImage img = new WebImage(ResimURL.InputStream);
                    FileInfo imginfo = new FileInfo(ResimURL.FileName);

                    string logoname = Guid.NewGuid().ToString() + imginfo.Extension;
                    img.Resize(500, 500);
                    img.Save("~/Uploads/Urun/" + logoname);

                    urun.ResimURL = "/Uploads/Urun/" + logoname;
                }
                db.Urun.Add(urun);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                ViewBag.Uyari = "Düzenlenecek Ürün Bulunamadı!";
            }
            var urun = db.Urun.Find(id);
            if (urun == null)
            {
                return HttpNotFound();
            }

            return View(urun);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(int? id,Urun urun, HttpPostedFileBase ResimURL)
        {
            
            if (ModelState.IsValid)
            {
                var u = db.Urun.Where(x => x.UrunId == id).SingleOrDefault();
                if (ResimURL!=null)
                {
                    if (System.IO.File.Exists(Server.MapPath(u.ResimURL)))
                    {
                        System.IO.File.Delete(Server.MapPath(u.ResimURL));
                    }

                    WebImage img = new WebImage(ResimURL.InputStream);
                    FileInfo imginfo = new FileInfo(ResimURL.FileName);

                    string urunname = Guid.NewGuid().ToString() + imginfo.Extension;
                    img.Resize(500, 500);
                    img.Save("~/Uploads/Urun/" + urunname);

                    u.ResimURL = "/Uploads/Urun/" + urunname;
                }
                u.Baslik = urun.Baslik;
                u.Aciklama = urun.Aciklama;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            var u = db.Urun.Find(id);
            if (u == null)
            {
                return HttpNotFound();
            }
            db.Urun.Remove(u);
            db.SaveChanges();
            return RedirectToAction("Index");
            
        }
    }
}