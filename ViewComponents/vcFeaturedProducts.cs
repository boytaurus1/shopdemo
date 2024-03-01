using Microsoft.AspNetCore.Mvc;
using ShopHoaMVC.Models;

namespace ShopHoaMVC.ViewComponents
{
    public class vcFeaturedProducts : ViewComponent
    {
        private readonly Hshop2023Context _db;

        public vcFeaturedProducts(Hshop2023Context db)
        {
            _db = db;
        }
        public IViewComponentResult Invoke()
        {
            var products = _db.HangHoas.OrderBy(x => Guid.NewGuid());
            return View(products.Take(3).ToList());
        }
    }
}
