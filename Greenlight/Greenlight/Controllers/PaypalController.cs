﻿using Greenlight.Models;
using Greenlight.Utilities;
using log4net.Repository.Hierarchy;
using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Order = Greenlight.Models.Order;

namespace Greenlight.Controllers
{
    [Authorize]
    public class PaypalController : Controller
    {
        private Payment payment;
        public ActionResult Index()
        {
            return View();
        }
        private Payment ExecutePayment(APIContext apiContext, string payerId, string paymentId)
        {
            PaymentExecution paymentExecution = new PaymentExecution() { payer_id = payerId };
            this.payment = new Payment() { id = paymentId };
            return this.payment.Execute(apiContext, paymentExecution);
        }

        private Payment CreatePayment(APIContext apiContext, string redirectUrl)
        {
            Order order = (Order)TempData["Order"];
            List<OrderDetail> orderDetails = order.OrderDetails;
            string format = "{0:F2}";

            List<Item> items = orderDetails.Select(cartItem => new Item
            {
                name = cartItem.Game.Name,
                currency = "USD",
                price = string.Format(format, cartItem.Game.Price)

            }).ToList();

            ItemList itemList = new ItemList
            {
                items = items
            };

            Payer payer = new Payer() { payment_method = "paypal" };

            decimal subtotal = order.Total;

            Details details = new Details
            {
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

            this.payment = new Payment()
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
            string payerId = Request.Params["PayerID"];

            if (string.IsNullOrEmpty(payerId))
            {
                string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority +
                            "/Paypal/PaymentWithPayPal?";

                var guid = Convert.ToString((new Random()).Next(100000));

                var createdPayment = this.CreatePayment(apiContext, baseURI + "guid=" + guid);

                List<Links>.Enumerator links = createdPayment.links.GetEnumerator();

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
                string guid = Request.Params["guid"];

                Payment executedPayment = ExecutePayment(apiContext, payerId, Session[guid] as string);

                if (executedPayment.state.ToLower() != "approved")
                {
                    return View("Failure");
                }
            }
            return View("Success");
        }
        
    }
}
