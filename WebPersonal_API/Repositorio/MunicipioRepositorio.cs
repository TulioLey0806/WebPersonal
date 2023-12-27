using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using WebPersonal_API.Datos;
using WebPersonal_API.Modelos;
using WebPersonal_API.Repositorio.IRepositorio;

namespace WebPersonal_API.Repositorio
{
    public class MunicipioRepositorio(PersonalDbContext db) : Repositorio<CMunici>(db), IMunicipioRepositorio
    {
        private readonly PersonalDbContext _db = db;

        public async Task<CMunici> Actualizar(CMunici entidad)
        {
            _db.CMunicis.Update(entidad);
            await _db.SaveChangesAsync();
            return entidad;
        }

        public string GetCodigoMunicipio(string codprovin)
        {
            var consecutivo = _db.CMunicis.Where(v=> v.CodProvin == codprovin).OrderByDescending(v => v.CodMunici).FirstOrDefault().CodMunici;

            //var consecutivo = (from sc in _db.CMunicis
            //                   where sc.CodProvin.Equals(codprovin)
            //                   orderby sc.CodProvin, sc.CodMunici descending
            //                   select sc.CodMunici).FirstOrDefault();

            if (consecutivo == null)
                return "01";

            int nuevoConsecutivo = Convert.ToInt32(consecutivo) + 1;
            return nuevoConsecutivo.ToString().PadLeft(2, '0');
        }
    }
}
