using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopHoaMVC.Models;
using ShopHoaMVC.Models.CustomerModels;
using ShopHoaMVC.TagHelper;
using System.Security.Claims;

namespace ShopHoaMVC.Controllers
{
    public class RegisterController : Controller
    {
        private readonly Hshop2023Context _db;
        private readonly IMapper _mp;

        public RegisterController(Hshop2023Context db, IMapper mp)
        {
            _db = db;
            _mp = mp;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(Register input)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var khachhang = _mp.Map<KhachHang>(input);
                    khachhang.RandomKey = Util.GenerateRandomkey();
                    khachhang.MatKhau = input.MatKhau.ToMd5Hash(khachhang.RandomKey);
                    khachhang.HieuLuc = true;
                    khachhang.VaiTro = 0;

                    _db.Add(khachhang);
                    _db.SaveChanges();
                    return RedirectToAction("Index", "Home");
                }
                catch (Exception)
                {

                    throw;
                }

            }

            return View(input);
        }

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Login input, string ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            if (input.Username != null && input.Password != null)
            {
                var khachHang = _db.KhachHangs.FirstOrDefault(x => x.MaKh == input.Username);
                if (khachHang == null)
                {
                    ModelState.AddModelError("loi", "Không có KH này");
                }
                else
                {
                    if (!khachHang.HieuLuc)
                    {
                        ModelState.AddModelError("loi", "KH đã bị khóa");
                    }
                    else
                    {
                        if (khachHang.MatKhau != input.Password.ToMd5Hash(khachHang.RandomKey))
                        {
                            ModelState.AddModelError("loi", "Sai mật khẩu");
                        }
                        else
                        {
                            var claims = new List<Claim>
                            {
                                new Claim(ClaimTypes.Email, khachHang.Email),
                                new Claim(ClaimTypes.Name, khachHang.HoTen),
                                new Claim(MySetting.ClaimCustomerId,khachHang.MaKh),

                                //claim - role động
                                new Claim(ClaimTypes.Role, "Customer")
                            };
                            var claimsIdentity = new ClaimsIdentity(
                                claims,
                                CookieAuthenticationDefaults.AuthenticationScheme);
                            var clamsPrincipal = new ClaimsPrincipal(claimsIdentity);
                            await HttpContext.SignInAsync(clamsPrincipal);

                            if (Url.IsLocalUrl(ReturnUrl))
                            {
                                return Redirect(ReturnUrl);
                            }
                            else
                            {
                                return RedirectToAction("Index", "Home");
                            }
                        }
                    }

                }
            }
            return View();
        }

        [Authorize]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public IActionResult Profile()
        {
            return View();
        }



    }
}
