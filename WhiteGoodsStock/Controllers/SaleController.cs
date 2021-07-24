using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WhiteGoodsStock.Models.Entity;

namespace WhiteGoodsStock.Controllers
{
    public class SaleController : Controller
    {
        // GET: Sale
        DBMvcStockEntities db = new DBMvcStockEntities();
        public ActionResult Index()
        {
            var sales = db.TBLSALES.ToList();
            return View(sales);
        }
        [HttpGet]
        public ActionResult NewSale()
        {
            //Products dropdown list
            List<SelectListItem> product = (from x in db.TBLPRODUCTS.ToList()
                                            select new SelectListItem
                                            {
                                                Text = x.name,
                                                Value = x.id.ToString()
                                            }).ToList();
            ViewBag.val1 = product;

            //Staff
            List<SelectListItem> staff = (from x in db.TBLSTAFF.ToList()
                                          select new SelectListItem
                                          {
                                              Text = x.name + " " + x.surname,
                                              Value = x.id.ToString()
                                          }).ToList();
            ViewBag.val2 = staff;

            //Customer
            List<SelectListItem> customer = (from x in db.TBLCUSTOMER.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = x.name + " "+ x.surname,
                                                 Value = x.id.ToString()
                                             }).ToList();
            ViewBag.val3 = customer;

            return View();
        }
        [HttpPost]
        public ActionResult NewSale(TBLSALES p)
        {
            var product = db.TBLPRODUCTS.Where(x => x.id == p.TBLPRODUCTS.id).FirstOrDefault();
            var customer = db.TBLCUSTOMER.Where(x => x.id == p.TBLCUSTOMER.id).FirstOrDefault();
            var staff = db.TBLSTAFF.Where(x => x.id == p.TBLSTAFF.id).FirstOrDefault();
            p.TBLPRODUCTS = product;
            p.TBLCUSTOMER = customer;
            p.TBLSTAFF = staff;
            p.date = DateTime.Parse(DateTime.Now.ToShortDateString());
            db.TBLSALES.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}