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
            return View(new NorthwindEntities().Products.Find(id));
        }
    }
}