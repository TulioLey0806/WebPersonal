using AutoMapper;
using WebPersonal_MVC.Models.Dto;

namespace WebPersonal_MVC
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<CCatcarDto, CCatcarCreateDto>().ReverseMap();
            CreateMap<CCatcarDto, CCatcarUpdateDto>().ReverseMap();

            CreateMap<CProvinDto, CProvinCreateDto>().ReverseMap();
            CreateMap<CProvinDto, CProvinUpdateDto>().ReverseMap();

            CreateMap<CMuniciDto, CMuniciCreateDto>().ReverseMap();
            CreateMap<CMuniciDto, CMuniciUpdateDto>().ReverseMap();
        }
    }
}
