using Microsoft.AspNetCore.Mvc;
using ShopHoaMVC.Models;

namespace ShopHoaMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly Hshop2023Context _db;

        public HomeController(Hshop2023Context db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var random = new Random();
            var db = _db.HangHoas.OrderBy(x => Guid.NewGuid()).Take(8).ToList();
            
            return View(db);
        }
    }
}
