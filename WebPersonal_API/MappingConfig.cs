using AutoMapper;
using WebPersonal_API.Modelos;
using WebPersonal_API.Modelos.Dto;

namespace WebPersonal_API
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<C_catcar, C_catcarDto>().ReverseMap();
            CreateMap<C_catcar, C_catcarCreateDto>().ReverseMap();
            CreateMap<C_catcar, C_catcarUpdateDto>().ReverseMap();
        }
    }
}
