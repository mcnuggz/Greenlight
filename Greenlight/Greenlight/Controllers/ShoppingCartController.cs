﻿using Greenlight.Models;
using Greenlight.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Greenlight.Controllers
{
    public class ShoppingCartController : Controller
    {
        GreenlightContext db = new GreenlightContext();
        // GET: ShoppingCart
        public ActionResult Index()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);
            var viewModel = new ShoppingCartViewModel
            {
                CartItems = cart.GetCartItems(),
                CartTotal = cart.GetTotal()
            };
            return View(viewModel);
        }

        public ActionResult AddToCart(int id)
        {
            var addedGame = db.Games.Single(g => g.ID == id);
            var cart = ShoppingCart.GetCart(this.HttpContext);
            cart.AddToCart(addedGame);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult RemoveFromCart(int id)
        {
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext()
                .GetUserManager<ApplicationUserManager>()
                .FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());

            var cart = ShoppingCart.GetCart(this.HttpContext);
            string itemName = db.Games.Single(i => i.ID == id).Name;

            int itemCount = cart.RemoveFromCart(id);
            var viewModel = new ShoppingCartViewModel
            {
                CartItems = cart.GetCartItems(),
                CartTotal = cart.GetTotal()
            };
            var results = new ShoppingCartRemoveViewModel
            {
                Message = $"{Server.HtmlEncode(itemName)} has been removed from the cart.",
                CartCount = cart.GetCount(),
                CartTotal = cart.GetTotal(),
                ItemCount = itemCount,
                DeleteID = id
            };
            return Json(results);
        }

        [ChildActionOnly]
        public ActionResult CartSummary()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);
            ViewData["CartCount"] = cart.GetCount();
            return PartialView("CartSummary");
        }
    }
}