using Microsoft.AspNetCore.Mvc.Rendering;
using WebPersonal_MVC.Models.Dto;

namespace WebPersonal_MVC.Models.ViewModel
{
    public class MunicipioViewModel
    {
        public MunicipioViewModel()
        {
            CMunici = new CMuniciCreateDto();
        }

        public CMuniciCreateDto CMunici {  get; set; }
        
        public IEnumerable<SelectListItem> ProvinciaList { get; set; }

    }
}
