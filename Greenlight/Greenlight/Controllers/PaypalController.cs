using Greenlight.Models;
using Greenlight.Utilities;
using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Order = Greenlight.Models.Order;

namespace Greenlight.Controllers
{
    public class PaypalController : Controller
    {
        private Payment payment;
        private Payment ExecutePayment(APIContext apiContext, string payerID, string paymentID)
        {
            var paymentExecution = new PaymentExecution() { payer_id = payerID };
            this.payment = new Payment() { id = paymentID };
            return this.payment.Execute(apiContext, paymentExecution);
        }
        private Payment CreatePayment(APIContext apiContext, string redirectUrl)
        {
            Order order = (Order)TempData["Order"];
            List<OrderDetail> orderDetails = order.OrderDetails;
            string format = "{0:F2}";
            decimal tax = 0m;
            List<Item> items = orderDetails.Select(cartItem => new Item
            {
                name = cartItem.Game.Name,
                currency = "USD",
                description = cartItem.Game.Name,
                price = string.Format(format, cartItem.Game.Price),
                //tax = string.Format(format, tax),
                quantity = cartItem.Quantity.ToString()
            }).ToList();
            ItemList itemList = new ItemList
            {
                items = items
            };
            Payer payer = new Payer() { payment_method = "paypal" };
            decimal subtotal = order.Total;
            //decimal total = subtotal + tax;
            Details details = new Details
            {
                tax = string.Format(format, tax),
                subtotal = string.Format(format, subtotal)
            };
            Amount amount = new Amount
            {
                currency = "USD",
                total = string.Format(format, subtotal),
                details = details
            };
            List<Transaction> transactionList = new List<Transaction>();
            transactionList.Add(new Transaction()
            {
                description = "Transaction Description",
                invoice_number = Common.GetRandomInvoiceNumber(),
                amount = amount,
                item_list = itemList

            });
            var redirectUrls = new RedirectUrls()
            {
                cancel_url = redirectUrl,
                return_url = redirectUrl
            };

            payment = new Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = transactionList,
                redirect_urls = redirectUrls
            };

            return payment.Create(apiContext);
        }

        public ActionResult PaymentWithPaypal()
        {
            APIContext apiContext = Configuration.GetAPIContext();
            try
            {
                string payerId = Request.Params["PayerID"];
                if (string.IsNullOrEmpty(payerId))
                {
                    string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority +
                                "/Paypal/PaymentWithPayPal?";
                    var guid = Convert.ToString((new Random()).Next(100000));
                    var createdPayment = this.CreatePayment(apiContext, baseURI + "guid=" + guid);
                    var links = createdPayment.links.GetEnumerator();
                    string paypalRedirectUrl = null;
                    while (links.MoveNext())
                    {
                        Links link = links.Current;
                        if (link.rel.ToLower().Trim().Equals("approval_url"))
                        {
                            paypalRedirectUrl = link.href;
                        }
                    }
                    Session.Add(guid, createdPayment.id);
                    return Redirect(paypalRedirectUrl);
                }
                else
                {
                    var guid = Request.Params["guid"];
                    var executedPayment = ExecutePayment(apiContext, payerId, Session[guid] as string);
                    if (executedPayment.state.ToLower() != "approved")
                    {
                        return View("Failure");
                    }
                }
            }
            catch
            {
                return View("Failure");
            }
            return View("Success");
        }
    }
}