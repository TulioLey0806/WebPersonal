using System.ComponentModel.DataAnnotations;

namespace WebPersonal_API.Modelos.Dto
{
    public class CCatcarCreateDto
    {
        [Display(Name = "Código Categoria")]
        public string CodCatcar { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(50, ErrorMessage = "El valor de {0} no puede superar los {1} caracteres")]
        [Display(Name = "Nombre Categoria")]
        public string NomCatcar { get; set; }
    }
}
