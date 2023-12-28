using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebPersonal_MVC.Models.Dto
{
    public class CMuniciCreateDto
    {
        [Display(Name = "Código Provincia")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Unicode(false)]
        public string CodProvin { get; set; }

        [Display(Name = "Código Municipio")]
        [Unicode(false)]
        public string CodMunici { get; set; }

        [Display(Name = "Nombre Municipio")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [StringLength(50, ErrorMessage = "El valor de {0} no puede superar los {1} caracteres.")]
        [Unicode(false)]
        public string NomMunici { get; set; }

        //[InverseProperty("CMunici")]
        //public virtual ICollection<CBarrio> CBarrios { get; set; } = new List<CBarrio>();

        //[ForeignKey("CodProvin")]
        //[InverseProperty("CMunicis")]
        //public virtual CProvin CodProvinNavigation { get; set; }
    }
}
