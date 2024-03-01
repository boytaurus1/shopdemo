using Microsoft.AspNetCore.Mvc;
using ShopHoaMVC.Models;
using ShopHoaMVC.Models.CustomerModels;

namespace ShopHoaMVC.ViewComponents
{
    public class vcShopCategories : ViewComponent
    {
        private readonly Hshop2023Context _db;
        public vcShopCategories(Hshop2023Context db)
        {
           _db = db;
        }
        public IViewComponentResult Invoke()
        {
            var data = _db.Loais.Where(w => w.Status == true).Select(x => new CategoryMenu
            {
                ID = x.MaLoai,
                Name = x.TenLoai,
                Quantity = x.HangHoas.Count
            }).ToList();
            return View(data);
        }
    }
}
