using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using WebPersonal_API.Datos;
using WebPersonal_API.Modelos;
using WebPersonal_API.Repositorio.IRepositorio;

namespace WebPersonal_API.Repositorio
{
    public class ProvinciaRepositorio(PersonalDbContext db) : Repositorio<CProvin>(db), IProvinciaRepositorio
    {
        private readonly PersonalDbContext _db = db;

        public async Task<CProvin> Actualizar(CProvin entidad)
        {
            _db.CProvins.Update(entidad);
            await _db.SaveChangesAsync();
            return entidad;
        }

        public string GetCodigoProvincia()
        {
            var consecutivo = _db.CProvins.OrderByDescending(v => v.CodProvin).FirstOrDefault().CodProvin;
            if (consecutivo == null)
                return "01";

            int nuevoConsecutivo = Convert.ToInt32(consecutivo) + 1;
            return nuevoConsecutivo.ToString().PadLeft(2, '0');
        }
    }
}
