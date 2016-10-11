using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NorthMVC.Models;

namespace NorthMVC.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        public ActionResult Index()
        {
            return View(new NorthwindEntities().Categories.ToList());
        }

        public ActionResult Detay(int? id)
        {
            if (id == null)
                return RedirectToAction("Index");
            var kategori = new NorthwindEntities().Categories.Find(id.Value);
            return View(kategori);
        }
        [HttpPost]
        public ActionResult Duzenle(Category guncellenenKategori)
        {
            NorthwindEntities db = new NorthwindEntities();
            var guncellenecek = db.Categories.Find(guncellenenKategori.CategoryID);
            guncellenecek.CategoryName = guncellenenKategori.CategoryName;
            guncellenecek.Description = guncellenenKategori.Description;
            db.SaveChanges();
            return View("Index",db.Categories.ToList());
        }

        public ActionResult Yeni()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Yeni(Category cat)
        {
            NorthwindEntities db = new NorthwindEntities();
            db.Categories.Add(cat);
            db.SaveChanges();
            return View("Index", db.Categories.ToList());
        }
    }
}