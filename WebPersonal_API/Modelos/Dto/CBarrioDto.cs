using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebPersonal_API.Modelos.Dto
{
    public class CBarrioDto
    {
        [Display(Name = "Código Barrio")]
        [Unicode(false)]
        public string CodBarrio { get; set; }

        [Display(Name = "Nombre Barrio")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [StringLength(50, ErrorMessage = "El valor de {0} no puede superar los {1} caracteres.")]
        [Unicode(false)]
        public string NomBarrio { get; set; }

        [Required]
        [Display(Name = "Municipio")]
        [StringLength(2, ErrorMessage = "El valor de {0} no puede superar los {1} caracteres.")]
        [Unicode(false)]
        public string CodMunici { get; set; }

        [Display(Name = "Provincia")]
        [StringLength(2, ErrorMessage = "El valor de {0} no puede superar los {1} caracteres.")]
        [Unicode(false)]
        public string CodProvin { get; set; }

        [ForeignKey("CodProvin, CodMunici")]
        [InverseProperty("CBarrios")]
        public virtual CMunici CMunici { get; set; }

        [ForeignKey("CodProvin")]
        [InverseProperty("CBarrios")]
        public virtual CProvin CodProvinNavigation { get; set; }
    }
}
