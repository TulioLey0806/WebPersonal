using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebPersonal_API.Modelos.Dto
{
    public class CProvinUpdateDto
    {
        [Display(Name = "Código Provincia")]
        [Unicode(false)]
        public string CodProvin { get; set; }

        [Display(Name = "Nombre Provincia")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [StringLength(50, ErrorMessage = "El valor de {0} no puede superar los {1} caracteres.")]
        [Unicode(false)]
        public string NomProvin { get; set; }

        //[InverseProperty("CodProvinNavigation")]
        //public virtual ICollection<CBarrio> CBarrios { get; set; } = new List<CBarrio>();

        //[InverseProperty("CodProvinNavigation")]
        //public virtual ICollection<CMunici> CMunicis { get; set; } = new List<CMunici>();
    }
}
