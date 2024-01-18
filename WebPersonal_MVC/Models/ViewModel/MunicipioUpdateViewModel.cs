 using Microsoft.AspNetCore.Mvc.Rendering;
using WebPersonal_MVC.Models.Dto;

namespace WebPersonal_MVC.Models.ViewModel
{
    public class MunicipioUpdateViewModel
    {
        public MunicipioUpdateViewModel()
        {
            CMunici = new CMuniciUpdateDto();
        }

        public CMuniciUpdateDto CMunici {  get; set; }
        
        public IEnumerable<SelectListItem> ProvinciaList { get; set; }

    }
}
