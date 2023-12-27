using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebPersonal_API.Modelos;

/// <summary>
/// Barrios
/// </summary>
[Table("c_barrio")]
public partial class CBarrio
{
    [Key]
    [Column("cod_barrio")]
    [StringLength(3)]
    [Unicode(false)]
    public string CodBarrio { get; set; }

    [Required]
    [Column("nom_barrio")]
    [StringLength(50)]
    [Unicode(false)]
    public string NomBarrio { get; set; }

    [Required]
    [Column("cod_munici")]
    [StringLength(2)]
    [Unicode(false)]
    public string CodMunici { get; set; }

    [Required]
    [Column("cod_provin")]
    [StringLength(2)]
    [Unicode(false)]
    public string CodProvin { get; set; }

    [ForeignKey("CodProvin, CodMunici")]
    [InverseProperty("CBarrios")]
    public virtual CMunici CMunici { get; set; }

    [ForeignKey("CodProvin")]
    [InverseProperty("CBarrios")]
    public virtual CProvin CodProvinNavigation { get; set; }
}
