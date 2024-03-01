using Microsoft.AspNetCore.Mvc;
using ShopHoaMVC.Models.CustomerModels;
using ShopHoaMVC.TagHelper;

namespace ShopHoaMVC.ViewComponents
{
    public class vcHeaderCartIcon : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var cart = HttpContext.Session.Get<List<CartItem>>(MySetting.CART_KEY) ?? new List<CartItem>();
            return View("Default", new CartCout
            {
                Quantity = cart.Sum(s => s.CartQuantity)
            });
        }
    }
}
