using Microsoft.AspNetCore.Mvc;

namespace ShopHoaMVC.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error")]
        public IActionResult Index()
        {
            // Kiểm tra xem có thông điệp trong TempData không
            if (TempData.ContainsKey("Message"))
            {
                // Lấy thông điệp từ TempData
                string message = TempData["Message"].ToString();

                // Xóa thông điệp khỏi TempData để tránh giữ lại
                TempData.Remove("Message");

                // Trả về View hoặc ActionResult khác nếu cần
                // Gán thông điệp vào ViewBag để sử dụng nó trong View
                ViewBag.Message = message;
            }
            return View();
        }
    }
}
