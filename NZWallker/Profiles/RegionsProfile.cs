using AutoMapper;

namespace NZWallker.API.Profiles
{
    public class RegionsProfile : Profile
    {
        public RegionsProfile() 
        {
            CreateMap<Models.Domain.Region, Models.DTO.Region>().ReverseMap();
        }
    }
}
