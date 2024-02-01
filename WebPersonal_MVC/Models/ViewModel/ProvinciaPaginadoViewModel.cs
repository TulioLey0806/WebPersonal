using WebPersonal_MVC.Models.Dto;

namespace WebPersonal_MVC.Models.ViewModel
{
    public class ProvinciaPaginadoViewModel
    {
        public int PageNumber { get; set; }

        public int TotalPaginas { get; set; }

        public string Previo { get; set; } = "disabled";

        public string Siguiente { get; set; } = "";

        public IEnumerable<CProvinDto> ProvinciaList { get; set; }
    }
}
