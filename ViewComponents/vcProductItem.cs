using Microsoft.AspNetCore.Mvc;
using ShopHoaMVC.Models;

namespace ShopHoaMVC.ViewComponents
{
    public class vcProductItem : ViewComponent
    {
        public IViewComponentResult Invoke(HangHoa item)
        {
            return View(item);
        }
    }
}
