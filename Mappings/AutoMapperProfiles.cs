using AutoMapper;
using FirstWebAPI.Models.Domain;
using FirstWebAPI.Models.DTO;

namespace FirstWebAPI.Mappings
{
    public class AutoMapperProfiles : Profile{
        public AutoMapperProfiles()
        {
            CreateMap<Region, RegionDto>().ReverseMap();
            CreateMap<AddRegionRequestDto,Region>().ReverseMap();
            CreateMap<UpdateRegionRequestDto,Region>().ReverseMap();
        }
    }
}