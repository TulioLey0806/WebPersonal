using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebPersonal_API.Modelos;

[PrimaryKey("CodProvin", "CodMunici")]
[Table("c_munici")]
public partial class CMunici
{
    [Key]
    [Column("cod_provin")]
    [StringLength(2)]
    [Unicode(false)]
    public string CodProvin { get; set; }

    [Key]
    [Column("cod_munici")]
    [StringLength(2)]
    [Unicode(false)]
    public string CodMunici { get; set; }

    [Required]
    [Column("nom_munici")]
    [StringLength(50)]
    [Unicode(false)]
    public string NomMunici { get; set; }

    [InverseProperty("CMunici")]
    public virtual ICollection<CBarrio> CBarrios { get; set; } = new List<CBarrio>();

    [ForeignKey("CodProvin")]
    [InverseProperty("CMunicis")]
    public virtual CProvin CodProvinNavigation { get; set; }
}
