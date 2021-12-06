using AutoMapper;
using Floward.OnlineStore.ApplicationCore.Dto;
using Floward.OnlineStore.Core.Models;

namespace Floward.OnlineStore.ApplicationCore.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDto>().ReverseMap();           
        }
    }
}
