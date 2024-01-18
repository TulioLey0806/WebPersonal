using Microsoft.AspNetCore.Mvc.Rendering;
using WebPersonal_MVC.Models.Dto;

namespace WebPersonal_MVC.Models.ViewModel
{
    public class MunicipioDeleteViewModel
    {
        public MunicipioDeleteViewModel()
        {
            CMunici = new CMuniciDto();
        }

        public CMuniciDto CMunici {  get; set; }
        
        public IEnumerable<SelectListItem> ProvinciaList { get; set; }

    }
}
