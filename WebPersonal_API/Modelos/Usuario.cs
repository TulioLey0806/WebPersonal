using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebPersonal_API.Modelos;

public partial class Usuario
{
    [Key]
    public int Id { get; set; }

    public string UserName { get; set; }

    public string Nombres { get; set; }

    public string Password { get; set; }

    public string Rol { get; set; }
}
