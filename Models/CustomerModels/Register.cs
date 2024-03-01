using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ShopHoaMVC.Models.CustomerModels
{
    public class Register
    {
        [Required(ErrorMessage = "*")]
        [Display(Name ="Tên đăng nhập")]
        public string MaKh { get; set; } = null!;
        [Required(ErrorMessage = "*")]
        [Display(Name ="Mật khẩu")]
        [DataType(DataType.Password)]
        public string MatKhau { get; set; }
        [Required(ErrorMessage = "*")]
        [MaxLength(50, ErrorMessage = "Tối đa 50 ký tự")]
        public string HoTen { get; set; } = null!;
        public bool? GioiTinh { get; set; } = true;

        public DateTime? NgaySinh { get; set; }

        public string? DiaChi { get; set; }
        [Required(ErrorMessage = "*")]
        public string DienThoai { get; set; }
        [Required(ErrorMessage = "*")]
        [EmailAddress(ErrorMessage ="chưa đúng dạng mail")]
        public string Email { get; set; } = null!;

    }
}
