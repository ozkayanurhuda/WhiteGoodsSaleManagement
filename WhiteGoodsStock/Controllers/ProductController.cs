using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WhiteGoodsStock.Models.Entity;

namespace WhiteGoodsStock.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        DBMvcStockEntities db = new DBMvcStockEntities();
        public ActionResult Index(string p)
        {
            //silinmeyen ürünlerin listesini döndür (False ise silinmiş)
            //var products = db.TBLPRODUCTS.Where(x => x.status == true).ToList();
            //var products = from x in db.TBLPRODUCTS select x;
            //başta true olanları listele ama arayınca true olmayan da çıkacak
            //ARAMA ÖZELLİĞİ
            var products = db.TBLPRODUCTS.Where(x=>x.status == true);
            if(!string.IsNullOrEmpty(p))
            {
                products = products.Where(x => x.name.Contains(p) && x.status == true);
            }
            return View(products.ToList());
        }

        [HttpGet]
        public ActionResult NewProduct()
        {
            //for dropdown list
            List<SelectListItem> ctg = (from x in db.TBLCATEGORY.ToList()
                                        select new SelectListItem
                                        {
                                            Text = x.name,
                                            Value = x.id.ToString()
                                        }).ToList();
            ViewBag.val = ctg;
            return View();
        }

        [HttpPost]
        public ActionResult NewProduct(TBLPRODUCTS p)
        {
            //viewbagden gelen degeri tutar
            var ctg = db.TBLCATEGORY.Where(x => x.id == p.TBLCATEGORY.id).FirstOrDefault();
            p.TBLCATEGORY = ctg;
            db.TBLPRODUCTS.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult BringProduct(int id )
        {
            //dropdown list 
            List<SelectListItem> category = (from x in db.TBLCATEGORY.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = x.name,
                                                 Value = x.id.ToString()
                                             }).ToList();
            var prd = db.TBLPRODUCTS.Find(id);
            ViewBag.val = category;
            return View("BringProduct", prd);
        }
        public ActionResult UpdateProduct(TBLPRODUCTS p)
        {
            var product = db.TBLPRODUCTS.Find(p.id);
            product.name = p.name;
            product.brand = p.brand;
            product.stock = p.stock;
            product.purchaseprice = p.purchaseprice;
            product.saleprice = p.saleprice;

            var ctg = db.TBLCATEGORY.Where(x => x.id == p.TBLCATEGORY.id).FirstOrDefault();
            product.category = ctg.id;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DeleteProduct(TBLPRODUCTS p)
        {
            //sildiğinde durumu false yap 
            var findprdct = db.TBLPRODUCTS.Find(p.id);
            findprdct.status = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}