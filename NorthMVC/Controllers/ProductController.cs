using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NorthMVC.Models;

namespace NorthMVC.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            return View(new NorthwindEntities().Products.ToList());
        }
        public ActionResult Detay(int? id)
        {
            if (id == null)
                return RedirectToAction("Index");
            NorthwindEntities db = new NorthwindEntities();
            var kategoriler = new List<SelectListItem>();
            kategoriler.Add(new SelectListItem()
            {
                Text = "Kategorisi Yok",
                Value = "null"
            });
            db.Categories.ToList().ForEach(x =>
                kategoriler.Add(new SelectListItem
                {
                    Text = x.CategoryName,
                    Value = x.CategoryID.ToString()
                })
            );
            ViewBag.Kategoriler = kategoriler;


            return View(db.Products.Find(id));
        }
        [HttpPost]
        public ActionResult Duzenle(Product urun)
        {
            NorthwindEntities db = new NorthwindEntities();
            var guncellenecek = db.Products.Find(urun.ProductID);
            guncellenecek.ProductName = urun.ProductName;
            guncellenecek.UnitPrice = urun.UnitPrice;
            guncellenecek.QuantityPerUnit = urun.QuantityPerUnit;
            guncellenecek.UnitsInStock = urun.UnitsInStock;
            guncellenecek.CategoryID = urun.CategoryID;
            db.SaveChanges();
            return RedirectToAction("Detay", new { id = urun.ProductID });
        }
        public ActionResult Yeni()
        {
            NorthwindEntities db = new NorthwindEntities();
            var kategoriler = new List<SelectListItem>();
            kategoriler.Add(new SelectListItem()
            {
                Text = "Kategorisi Yok",
                Value = "null"
            });
            db.Categories.ToList().ForEach(x =>
                kategoriler.Add(new SelectListItem
                {
                    Text = x.CategoryName,
                    Value = x.CategoryID.ToString()
                })
            );
            ViewBag.Kategoriler = kategoriler;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Yeni(Product urun)
        {
            NorthwindEntities db = new NorthwindEntities();
            var kategoriler = new List<SelectListItem>();
            kategoriler.Add(new SelectListItem()
            {
                Text = "Kategorisi Yok",
                Value = "null"
            });
            db.Categories.ToList().ForEach(x =>
                kategoriler.Add(new SelectListItem
                {
                    Text = x.CategoryName,
                    Value = x.CategoryID.ToString()
                })
            );
            ViewBag.Kategoriler = kategoriler;

            if (!ModelState.IsValid)
                return View(urun);
            try
            {
                db.Products.Add(urun);
                db.SaveChanges();
                return RedirectToAction("Detay", new { id = urun.ProductID });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(urun);
            }
        }

        public JsonResult UrunDetay(int? id)
        {
            if (id == null)
            {
                return Json(new
                {
                    success = false
                }, JsonRequestBehavior.AllowGet);
            }
            NorthwindEntities db = new NorthwindEntities();
            var urun = db.Products.Find(id.Value);
            return Json(new
            {
                success = true,
                data = new
                {
                    id = urun.ProductID,
                    name = urun.ProductName,
                    price = urun.UnitPrice
                }
            }, JsonRequestBehavior.AllowGet);
        }
    }
}