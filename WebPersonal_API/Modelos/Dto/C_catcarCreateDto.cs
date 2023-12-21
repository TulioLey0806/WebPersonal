using System.ComponentModel.DataAnnotations;

namespace WebPersonal_API.Modelos.Dto
{
    public class C_catcarCreateDto
    {
        //[Required(ErrorMessage = "El campo Nom_catcar es obligatorio")]
        //[MaxLength(2, ErrorMessage = "El campo Cod_catcar solo tiene una longitud de 2 caracteres")]
        [Display(Name = "Código Categoria")]
        public string Cod_catcar { get; set; }

        [Required(ErrorMessage = "El campo Nom_catcar es obligatorio")]
        [MaxLength(50, ErrorMessage = "El campo Nom_catcar solo tiene una longitud de 50 caracteres")]
        [Display(Name = "Nombre Categoria")]
        public string Nom_catcar { get; set; }
    }
}
