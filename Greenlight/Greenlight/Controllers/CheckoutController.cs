using Greenlight.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Greenlight.Controllers
{
    [Authorize]
    public class CheckoutController : Controller
    {
        GreenlightContext db = new GreenlightContext();
        const string PromoCode = "Free";
        public ActionResult AddressAndPayment()
        {
            return View();
        }

        //
        // GET: /Checkout/AddressAndPayment
        [HttpPost]
        public ActionResult AddressAndPayment(FormCollection values)
        {
            var order = new Order();
            TryUpdateModel(order);
            try
            {
                if (string.Equals(values["PromoCode"], PromoCode, StringComparison.OrdinalIgnoreCase) == false)
                {
                    return View(order);
                }
                order.Username = User.Identity.Name;
                order.OrderDate = DateTime.Now;
                db.Orders.Add(order);
                db.SaveChanges();
                var cart = ShoppingCart.GetCart(this.HttpContext);
                cart.CreateOrder(order);
                return RedirectToAction("Complete", new { id = order.Id });
            }
            catch 
            {
                return View(order);
            }
        }

        public ActionResult Complete(int id)
        {
            bool isValid = db.Orders.Any(o => o.Id == id && o.Username == User.Identity.Name);
            if (isValid)
            {
                return View(id);
            }
            else
            {
                return View("Error");
            }
        }
    }
}