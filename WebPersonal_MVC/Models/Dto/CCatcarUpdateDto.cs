using System.ComponentModel.DataAnnotations;

namespace WebPersonal_MVC.Models.Dto
{
    public class CCatcarUpdateDto
    {
        //[Required(ErrorMessage = "El campo Nom_catcar es obligatorio")]
        [MaxLength(2, ErrorMessage = "El valor de {0} no puede superar los {1} caracteres")]
        [Display(Name="Código Categoria")]
        public string CodCatcar { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(50, ErrorMessage = "El valor de {0} no puede superar los {1} caracteres")]
        [Display(Name = "Nombre Categoria")]
        public string NomCatcar { get; set; }
    }
}
