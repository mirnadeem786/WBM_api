using AutoMapper;
using WillowBatMarketWebApiService.Entity;

namespace WillowBatMarketWebApiService.Models
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ManufacturerModel, Manufacturer>();
            CreateMap<BatModel, Bat>();
            CreateMap<WillowModel,Willow>();
            CreateMap<CricketerDashBoardModel, CricketerDashboard>();
            CreateMap<CartModel, Cart>();
        }
    }
}