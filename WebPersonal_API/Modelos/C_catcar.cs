using System.ComponentModel.DataAnnotations;

namespace WebPersonal_API.Modelos
{
    public class C_catcar
    {
        [Key]
        [MaxLength(2)]
        public string Cod_catcar { get; set; }

        [Required]
        [MaxLength(50)]
        public string Nom_catcar { get; set; }
    }
}
