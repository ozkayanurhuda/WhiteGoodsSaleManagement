using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WhiteGoodsStock.Models.Entity;

namespace WhiteGoodsStock.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        DBMvcStockEntities db = new DBMvcStockEntities();
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult NewAdmin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult NewAdmin(TBLADMIN p)
        {
            db.TBLADMIN.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}