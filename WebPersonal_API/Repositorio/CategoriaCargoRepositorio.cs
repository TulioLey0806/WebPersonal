﻿using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using WebPersonal_API.Datos;
using WebPersonal_API.Modelos;
using WebPersonal_API.Repositorio.IRepositorio;

namespace WebPersonal_API.Repositorio
{
    public class CategoriaCargoRepositorio : Repositorio<CCatcar>, ICategoriaCargoRepositorio
    {
        private readonly PersonalDbContext _db;

        public CategoriaCargoRepositorio(PersonalDbContext db) :base(db)
        {
            _db = db;
        }
 
        public async Task<CCatcar> Actualizar(CCatcar entidad)
        {
            _db.CCatcars.Update(entidad);
            await _db.SaveChangesAsync();
            return entidad;
        }

        public string GetCodigoCategoriaCargos()
        {
            var consecutivo = _db.CCatcars.OrderByDescending(v => v.CodCatcar).FirstOrDefault().CodCatcar;
            if (consecutivo == null)
                return "01";

            int nuevoConsecutivo = Convert.ToInt32(consecutivo) + 1;
            return nuevoConsecutivo.ToString().PadLeft(2, '0');
        }
    }
}
