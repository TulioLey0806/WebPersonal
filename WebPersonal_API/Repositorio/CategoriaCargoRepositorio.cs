using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using WebPersonal_API.Datos;
using WebPersonal_API.Modelos;
using WebPersonal_API.Repositorio.IRepositorio;

namespace WebPersonal_API.Repositorio
{
    public class CategoriaCargoRepositorio : Repositorio<C_catcar>, ICategoriaCargoRepositorio
    {
        private readonly ApplicationDbContext _db;

        public CategoriaCargoRepositorio(ApplicationDbContext db) :base(db)
        {
            _db = db;
        }
 
        public async Task<C_catcar> Actualizar(C_catcar entidad)
        {
            _db.C_catcar.Update(entidad);
            await _db.SaveChangesAsync();
            return entidad;
        }

        public string GetCodigoCategoriaCargos()
        {
            var consecutivo = _db.C_catcar.OrderByDescending(v => v.Cod_catcar).FirstOrDefault().Cod_catcar;
            if (consecutivo == null)
                return "01";

            int nuevoConsecutivo = Convert.ToInt32(consecutivo) + 1;
            return nuevoConsecutivo.ToString().PadLeft(2, '0');
        }
    }
}
