using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopHoaMVC.Models;
using ShopHoaMVC.Models.CustomerModels;
using ShopHoaMVC.TagHelper;

namespace ShopHoaMVC.Controllers
{

    public class CartController : Controller
    {
        private readonly Hshop2023Context _db;
        public CartController(Hshop2023Context db)
        {
            _db = db;
        }
        //gọi biến global CART_KEY 
        public List<CartItem> Cart => HttpContext.Session.Get<List<CartItem>>(MySetting.CART_KEY) ?? new List<CartItem>();
        public IActionResult Index()
        {
            return View(Cart);
        }
        public IActionResult AddToCart(int id, int quantity = 1)
        {
            var cart = Cart;
            var item = cart.SingleOrDefault(p => p.CartId == id);
            if (item == null)
            {
                var product = _db.HangHoas.SingleOrDefault(x => x.MaHh == id);
                if (product == null)
                {
                    TempData["Message"] = $"Không tìm thấy sản phẩm có mã {id}";
                    return RedirectToAction("Index", "Error");
                }
                item = new CartItem
                {
                    CartId = product.MaHh,
                    CartTitle = product.TenHh,
                    CartImage = product.Hinh ?? "",
                    CartPrice = product.DonGia ?? 0,
                    CartQuantity = quantity
                };
                cart.Add(item);
            }
            else
            {
                item.CartQuantity += quantity;
            }
            HttpContext.Session.Set(MySetting.CART_KEY, cart);

            return RedirectToAction("Index", "Cart");
        }
        public IActionResult RemoveCart(int id)
        {
            var cart = Cart;
            var item = cart.SingleOrDefault(p => p.CartId == id);
            if (item != null)
            {
                cart.Remove(item);
                HttpContext.Session.Set(MySetting.CART_KEY, cart);
            }

            return RedirectToAction("Index", "Cart");
        }

        [Authorize]
        [HttpGet]
        public IActionResult CheckOut()
        {
            var cart = Cart;
            if (cart.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(Cart);
        }

        [Authorize]
        [HttpPost]
        public IActionResult CheckOut(CheckOutInfo input ,bool isGiongKhachHang)
        {
            // lấy customerId(Makh) được lưu giữ trong claims khi login
            var cusId = HttpContext.User.Claims.SingleOrDefault(x => x.Type == MySetting.ClaimCustomerId).Value;
            var customer = new KhachHang();
            if (input.IsCheckedCustomerInfo)
            {
                customer = _db.KhachHangs.SingleOrDefault(x => x.MaKh == cusId);
            }
            var bill = new HoaDon()
            {
                MaKh = cusId,
                HoTen = input.FullName ?? customer.HoTen,
                DiaChi = input.Address ?? customer.DiaChi,
                DienThoai = input.Phone ?? customer.DienThoai,
                CachThanhToan = "COD",
                CachVanChuyen = "GRAB",
                PhiVanChuyen = 10000,
                MaTrangThai = 0,
                GhiChu = input.Note
            };

            _db.Database.BeginTransaction();
            try
            {
                _db.Database.CommitTransaction();
                _db.Add(bill);
                _db.SaveChanges();
                var billDetail = new List<ChiTietHd>();

                foreach (var item in Cart)
                {
                    billDetail.Add(new ChiTietHd()
                    {
                        MaHd = bill.MaHd,
                        MaHh = item.CartId,
                        DonGia = item.CartPrice,
                        SoLuong = item.CartQuantity,
                        GiamGia = 0.0
                    });
                }
                _db.AddRange(billDetail);
                _db.SaveChanges();
                HttpContext.Session.Set<List<CartItem>>(MySetting.ClaimCustomerId, new List<CartItem>());

                //tạm thời thành công thì coi như thành công.
                return RedirectToAction("Success", "Cart");
            }
            catch
            {
                _db.Database.RollbackTransaction();
            }

            return View(Cart);
        }

        public IActionResult Success()
        {
            return View(Cart);
        }
    }
}
