using System.Text;

namespace ShopHoaMVC.TagHelper
{
    public class Util
    {
        public static string UploadHinh(IFormFile Hinh, string folder)
        {
            try
            {
                var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Hinh", folder, Hinh.FileName);
                using (var myfile = new FileStream(fullPath, FileMode.CreateNew))
                {
                    Hinh.CopyTo(myfile);
                }
                return Hinh.FileName;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }
        public static string GenerateRandomkey(int lenght = 5)
        {
            var patten = @"abcdefghijklmnopqrstuvwxyz0123456789";
            var sb = new StringBuilder();
            var rd = new Random();
            for (int i = 0; i < lenght; i++)
            {
                sb.Append(patten[rd.Next(0,patten.Length)]);
            }
            return sb.ToString();
        }
    }
}
