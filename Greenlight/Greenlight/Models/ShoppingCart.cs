using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Greenlight.Models
{
    public partial class ShoppingCart
    {
        GreenlightContext db = new GreenlightContext();
        string ShoppingCartID { get; set; }
        public const string CartSessionKey = "CartID";
        public static ShoppingCart GetCart(HttpContextBase context)
        {
            var cart = new ShoppingCart();
            cart.ShoppingCartID = cart.GetCartID(context);
            return cart;
        }

        public string GetCartID(HttpContextBase context)
        {
            if (context.Session[CartSessionKey] == null)
            {
                if (!string.IsNullOrWhiteSpace(context.User.Identity.Name))
                {
                    context.Session[CartSessionKey] = context.User.Identity.Name;
                }
                else
                {
                    Guid tempCartID = Guid.NewGuid();
                    context.Session[CartSessionKey] = tempCartID.ToString();
                }
            }
            return context.Session[CartSessionKey].ToString();
        }

        public static ShoppingCart GetCart(Controller controller)
        {
            return GetCart(controller.HttpContext);
        }

        public void AddToCart(Game game)
        {
            var cartItem = db.Carts.SingleOrDefault(c => c.CartID == ShoppingCartID && c.GameID == game.ID);
            if (cartItem == null)
            {
                cartItem = new Cart
                {
                    GameID = game.ID,
                    CartID = ShoppingCartID,
                    Count = 1,
                    DateCreated = DateTime.Now
                };
                db.Carts.Add(cartItem);
            }
            else
            {
                cartItem.Count++;
            }
            db.SaveChanges();
        }

        public int RemoveFromCart(int id)
        {
            var cartItem = db.Carts.Single(cart => cart.CartID == ShoppingCartID && cart.GameID == id);
            int itemCount = 0;
            if (cartItem == null)
            {
                if (cartItem.Count > 1)
                {
                    cartItem.Count--;
                    itemCount = cartItem.Count;
                }
                else
                {
                    db.Carts.Remove(cartItem);
                }
                db.SaveChanges();
            }
            return itemCount;
        }

        public void EmptyCart()
        {
            var cartItems = db.Carts.Where(c => c.CartID == ShoppingCartID);
            foreach (var cartItem in cartItems)
            {
                db.Carts.Remove(cartItem);
            }
            db.SaveChanges();
        }

        public List<Cart> GetCartItems()
        {
            return db.Carts.Where(c => c.CartID == ShoppingCartID).ToList();
        }

        public int GetCount()
        {
            int? count = (from cartItems in db.Carts where cartItems.CartID == ShoppingCartID select (int?)cartItems.Count).Sum();
            return count ?? 0;
        }

        public decimal GetTotal()
        {
            decimal? total = (from cartItems in db.Carts where cartItems.CartID == ShoppingCartID select (int?)cartItems.Count * cartItems.Game.Price).Sum();
            return total ?? decimal.Zero;
        }

        public int CreateOrder(Order order)
        {
            decimal orderTotal = 0;
            var cartItems = GetCartItems();
            foreach (var item in cartItems)
            {
                var orderDetail = new OrderDetail
                {
                    GameID = item.GameID,
                    OrderID = order.Id,
                    Price = item.Game.Price,
                    Quantity = item.Count
                };
                order.Total += (item.Count * item.Game.Price);
                db.OrderDetails.Add(orderDetail);
            }
            order.Total = orderTotal;
            db.SaveChanges();
            EmptyCart();
            return order.Id;
        }
        public void MoveCart(string userName)
        {
            var shoppingCart = db.Carts.Where(c => c.CartID == ShoppingCartID);
            foreach (Cart item in shoppingCart)
            {
                item.CartID = userName;
            }
            db.SaveChanges();
        }
    }
}