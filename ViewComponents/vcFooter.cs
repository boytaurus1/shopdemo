using Microsoft.AspNetCore.Mvc;

namespace ShopHoaMVC.ViewComponents
{
    public class vcFooter : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
