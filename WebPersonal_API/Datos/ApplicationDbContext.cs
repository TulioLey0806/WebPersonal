using Microsoft.EntityFrameworkCore;
using WebPersonal_API.Modelos;

namespace WebPersonal_API.Datos
{
    public class ApplicationDbContext :DbContext
    {
        // Implementando Inyección de Dependencia
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {
        
        }

//        public DbSet<C_catcar> C_catcar {  get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<C_catcar>().HasData(
        //        new C_catcar() { 
        //            Cod_catcar = "05",
        //            Nom_catcar = "Prueba desde VStudio"
        //            }
        //        );
        //}

    }




}
