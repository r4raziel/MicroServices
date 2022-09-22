using AutoMapper;
using PaltformService.Dtos;
using PaltformService.Models;

namespace PaltformService.Profiles
{

    public class PlatformsProfile : Profile
    {

        public PlatformsProfile()
        {
            //Source --> Target
            CreateMap<Platform, PlatformReadDto>();
            CreateMap<PlatformCreateDto, Platform>();
        }
    }

}