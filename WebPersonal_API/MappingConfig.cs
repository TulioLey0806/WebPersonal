using AutoMapper;
using WebPersonal_API.Modelos;
using WebPersonal_API.Modelos.Dto;

namespace WebPersonal_API
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<CCatcar, CCatcarDto>().ReverseMap();
            CreateMap<CCatcar, CCatcarCreateDto>().ReverseMap();
            CreateMap<CCatcar, CCatcarUpdateDto>().ReverseMap();

            CreateMap<CProvin, CProvinDto>().ReverseMap();
            CreateMap<CProvin, CProvinCreateDto>().ReverseMap();
            CreateMap<CProvin, CProvinUpdateDto>().ReverseMap();

            CreateMap<CMunici, CMuniciDto>().ReverseMap();
            CreateMap<CMunici, CMuniciCreateDto>().ReverseMap();
            CreateMap<CMunici, CMuniciUpdateDto>().ReverseMap();    

            //CreateMap<CBarrio, CBarrioDto>().ReverseMap();

        }
    }
}
