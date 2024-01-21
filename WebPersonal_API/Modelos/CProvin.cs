using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebPersonal_API.Modelos;

[Table("c_provin")]
public partial class CProvin
{
    [Key]
    [Column("cod_provin")]
    [StringLength(2)]
    [Unicode(false)]
    public string CodProvin { get; set; }

    [Required]
    [Column("nom_provin")]
    [StringLength(50)]
    [Unicode(false)]
    public string NomProvin { get; set; }

    [InverseProperty("CodProvinNavigation")]
    public virtual ICollection<CBarrio> CBarrios { get; set; } = new List<CBarrio>();

    [InverseProperty("CodProvinNavigation")]
    public virtual ICollection<CMunici> CMunicis { get; set; } = new List<CMunici>();
}
