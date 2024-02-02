using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebPersonal_API.Modelos
{
    public class UsuarioAplicacion : IdentityUser
    {
        [StringLength(11, ErrorMessage = "El valor {0} no puede superar los {1} caracteres.")]
        [Column(TypeName = "char")]
        public string Cod_ident { get; set; }

        [StringLength(5, ErrorMessage = "El valor {0} no puede superar los {1} caracteres.")]
        [Column(TypeName = "char")]
        public string Cod_reeup { get; set; }
    }
}
