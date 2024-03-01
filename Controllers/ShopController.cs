using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopHoaMVC.Models;
using ShopHoaMVC.Models.CustomerModels;

namespace ShopHoaMVC.Controllers
{
    public class ShopController : Controller
    {
        private readonly Hshop2023Context _db;

        public ShopController(Hshop2023Context db)
        {
            _db = db;
        }
        
        public IActionResult Index([FromRoute]int? id)
        {
            var db = _db.HangHoas.AsQueryable();
            if (id.HasValue)
            {
                db = db.Where(x => x.MaLoai == id.Value);
            }
            
            return View(db.ToList());
        }
        public IActionResult Search(string query)
        {
            var db = _db.HangHoas.AsQueryable();
            if (query != null)
            {
                db = db.Where(x => x.TenHh.Contains(query));
            }
            return View(db.ToList());
        }

        public IActionResult Detail(int loai)
        {
            //var db = _db.HangHoas.Where(x => x.MaHh == loai)
            //                     .Join(_db.Loais,
            //                           hh => hh.MaLoai,
            //                           loai => loai.MaLoai,
            //                           (hh,loai) => new ProductDetail
            //                           {
            //                               ID = hh.MaHh,
            //                               CatId = hh.MaLoai,
            //                               CatName = loai.TenLoai,
            //                               Description = hh.MoTa ?? "",
            //                               ShortDescription = hh.MoTaDonVi ?? "",
            //                               Image = hh.Hinh ?? "",
            //                               Price = hh.DonGia,
            //                               Title = hh.TenHh
            //                           });
            var db = _db.HangHoas
                        .Include(p => p.MaLoaiNavigation).SingleOrDefault(x => x.MaHh == loai);
            var result = new ProductDetail
            {
                ID = db.MaHh,
                CatId = db.MaLoai,
                CatName = db.MaLoaiNavigation.TenLoai,
                Description = db.MoTa ?? "",
                ShortDescription = db.MoTaDonVi ?? "",
                Image = db.Hinh ?? "",
                Price = db.DonGia,
                Title = db.TenHh
            };
            if (result == null )
            {
                TempData["Message"] = $"Không tìm thấy sản phẩm có mã {loai}";
                return RedirectToAction("Index", "Error");
            }
            
            return View(result);
        }
    }
}
