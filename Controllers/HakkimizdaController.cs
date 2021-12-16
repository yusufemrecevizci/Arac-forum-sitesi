﻿using Odev.Models.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Odev.Controllers
{
    public class HakkimizdaController : Controller
    {
        AraçKiralamaDBContext db = new AraçKiralamaDBContext();
        // GET: Hakkimizda
        public ActionResult Index()
        {
            var h = db.Hakkimizda.ToList();
            return View(h);
        }
    }
}