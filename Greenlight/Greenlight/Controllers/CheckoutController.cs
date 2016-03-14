using Greenlight.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Greenlight.Controllers
{
    [Authorize]
    public class CheckoutController : Controller
    {
        GreenlightContext db = new GreenlightContext();
        const string PromoCode = "FreeTax";
        public ActionResult AddressAndPayment()
        {
            Order previousOrder = db.Orders.FirstOrDefault(x => x.Username == User.Identity.Name);
            return View();
        }

        //
        // GET: /Checkout/AddressAndPayment
        [HttpPost]
        public ActionResult AddressAndPayment(FormCollection values)
        {
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext()
                .GetUserManager<ApplicationUserManager>()
                .FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            Order order = new Order();
            TryUpdateModel(order);
            try
            {
                order.Username = User.Identity.Name;
                order.Email = user.Email;
                order.OrderDate = DateTime.Now;
                string currentUserId = User.Identity.GetUserId();

                db.Orders.Add(order);
                db.SaveChanges();
                ShoppingCart cart = ShoppingCart.GetCart(this.HttpContext);
                cart.CreateOrder(order);

                TempData["Order"] = order;
                return RedirectToAction("PaymentWithPaypal", "Paypal");
            }
            catch (Exception)
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