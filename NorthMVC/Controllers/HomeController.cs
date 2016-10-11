using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NorthMVC.Models;

namespace NorthMVC.Controllers
{
    public class HomeController : Controller
    {

        // GET: Home
        public ActionResult Index()
        {
            NorthwindEntities db = new NorthwindEntities();
            var sonuc = db.Categories.ToList();
            return View(sonuc);
        }
    }
}