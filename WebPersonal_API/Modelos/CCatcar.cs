using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebPersonal_API.Modelos;

[Table("c_catcar")]
public partial class CCatcar
{
    [Key]
    [Column("cod_catcar")]
    [StringLength(2)]
    [Unicode(false)]
    public string CodCatcar { get; set; }

    [Required]
    [Column("nom_catcar")]
    [StringLength(50)]
    [Unicode(false)]
    public string NomCatcar { get; set; }
}
