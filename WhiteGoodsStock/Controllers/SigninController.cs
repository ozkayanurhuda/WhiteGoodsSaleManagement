using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WhiteGoodsStock.Models.Entity;
using System.Web.Security;

namespace WhiteGoodsStock.Controllers
{
    public class SigninController : Controller
    {
        // GET: Signin
        DBMvcStockEntities db = new DBMvcStockEntities();
        public ActionResult Signin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Signin(TBLADMIN p)
        {
            var infos = db.TBLADMIN.FirstOrDefault(x => x.user == p.user && x.password == p.password);
            if(infos!=null)
            {
                //yetkilendirme kullanıcı adı
                FormsAuthentication.SetAuthCookie(infos.user, false);
                return RedirectToAction("Index", "Customer");//customerdaki indexe 
            } else
            {
                return View();
            }
        }
    }
}