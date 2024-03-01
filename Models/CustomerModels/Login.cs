using System.ComponentModel.DataAnnotations;

namespace ShopHoaMVC.Models.CustomerModels
{
    public class Login
    {
        [Required(ErrorMessage = "Tên đăng nhập chưa đúng")]
        [Display(Name = "Tên đăng nhập")]
        public string Username { get; set; } = null!;
        [Required(ErrorMessage = "Mật khẩu chưa đúng")]
        [Display(Name = "Mật khẩu")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
