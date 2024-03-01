using AutoMapper;
using ShopHoaMVC.Models;
using ShopHoaMVC.Models.CustomerModels;

namespace ShopHoaMVC.TagHelper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Register, KhachHang>();
                //.ForMember(x => x.HoTen, opt => opt.MapFrom(register => register.HoTen));
        }
    }
}
