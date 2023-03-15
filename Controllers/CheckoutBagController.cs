using Microsoft.AspNetCore.Mvc;
using Mission9_awinder7.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mission9_awinder7.Controllers
{
    public class CheckoutBagController : Controller
    {
        private ICheckoutRepository repo { get; set; }
        private Basket basket { get; set; }
        public CheckoutBagController(ICheckoutRepository temp, Basket b)
        {
            repo = temp;
            basket = b;
        }

        [HttpGet]
        public IActionResult Checkout()
        {
            return View(new CheckoutBag());
        }

        [HttpPost]
        public IActionResult Checkout(CheckoutBag checkout)
        {
if (basket.Items.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry, your basket is empty!");
            }
            if (ModelState.IsValid)
            {
                checkout.Lines = basket.Items.ToArray();
                repo.SaveCheckout(checkout);
                basket.ClearBasket();
                return RedirectToPage("/Completed");
            }
            else
            {
                return View();
            }
        }
    }
}
