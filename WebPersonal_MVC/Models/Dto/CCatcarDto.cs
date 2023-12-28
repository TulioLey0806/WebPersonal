using System.ComponentModel.DataAnnotations;

namespace WebPersonal_MVC.Models.Dto
{
    public class CCatcarDto
    {
        [Display(Name="Código Categoria")]
        public string CodCatcar { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [StringLength(50, ErrorMessage = "El valor de {0} no puede superar los {1} caracteres.")]
        [Display(Name = "Nombre Categoria")]
        public string NomCatcar { get; set; }
    }
}
