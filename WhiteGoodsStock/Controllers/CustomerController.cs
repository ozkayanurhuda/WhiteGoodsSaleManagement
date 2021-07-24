using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WhiteGoodsStock.Models.Entity;
using PagedList.Mvc;
using PagedList;

namespace WhiteGoodsStock.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        DBMvcStockEntities db = new DBMvcStockEntities();
        [Authorize]
        public ActionResult Index(int page=1)
        {
            // var customerlist = db.TBLCUSTOMER.ToList();
            var customerlist = db.TBLCUSTOMER.Where(x=>x.status==true).ToList().ToPagedList(page, 3);
            return View(customerlist);
        }
        [HttpGet]
        public ActionResult NewCustomer ()
        {
            return View();
        }
        [HttpPost]
        public ActionResult NewCustomer(TBLCUSTOMER p)
        {
            //eklerken true yapıyoruz 
            if(!ModelState.IsValid)
            {
                return View("NewCustomer");
            }
            p.status = true;
            db.TBLCUSTOMER.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DeleteCustomer(TBLCUSTOMER p)
        {
            var findcustomer = db.TBLCUSTOMER.Find(p.id);
            findcustomer.status = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult BringCustomer(int id )
        {
            var customer = db.TBLCUSTOMER.Find(id);
            return View("BringCustomer", customer);
        } 

        public ActionResult UpdateCustomer(TBLCUSTOMER p)
        {
            var customer = db.TBLCUSTOMER.Find(p.id);
            customer.name = p.name;
            customer.surname = p.surname;
            customer.city = p.city;
            customer.balance = p.balance;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}