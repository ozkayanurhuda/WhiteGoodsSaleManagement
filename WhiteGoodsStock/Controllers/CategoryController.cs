using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WhiteGoodsStock.Models.Entity;

namespace WhiteGoodsStock.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        DBMvcStockEntities db = new DBMvcStockEntities();
        public ActionResult Index()
        {
            //butun değişkenleri almak için var tanımladım
            var categories = db.TBLCATEGORY.ToList();
            return View(categories);
        }
        [HttpGet] 
        public ActionResult NewCategory()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NewCategory(TBLCATEGORY p)
        {
            db.TBLCATEGORY.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DeleteCategory(int id)
        {
            var ctg = db.TBLCATEGORY.Find(id);
            db.TBLCATEGORY.Remove(ctg);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult BringCategory(int id)
        {
            var ctg = db.TBLCATEGORY.Find(id);
            return View("BringCategory", ctg);
        }

        public ActionResult UpdateCategory(TBLCATEGORY p)
        {
            var ctg = db.TBLCATEGORY.Find(p.id);
            ctg.name = p.name;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}