﻿using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebPersonal_API.Datos;
using WebPersonal_API.Modelos.Especificaciones;
using WebPersonal_API.Repositorio.IRepositorio;

namespace WebPersonal_API.Repositorio
{
    // Implementando Repositorio Generico
    public class Repositorio<T> : IRepositorio<T> where T : class
    {
        private readonly PersonalDbContext _db;
        internal DbSet<T> dbSet;
        
        public Repositorio(PersonalDbContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>();
        }

        public async Task Crear(T entidad)
        {
            await dbSet.AddAsync(entidad);
            await Grabar();
        }

        public async Task Grabar()
        {
            await _db.SaveChangesAsync();
        }

        public async Task<T> Obtener(Expression<Func<T, bool>> filtro = null, bool tracked = true, string? incluirPropiedades = null)
        {
            IQueryable<T> query = dbSet;
            if (!tracked)
            {
                query = query.AsNoTracking();
            }
            if(filtro != null)
            {
                query = query.Where(filtro);                   
            }
            if (incluirPropiedades !=null)
            {
                foreach (var incluirPro in incluirPropiedades.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(incluirPro);
                }
            }
            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<T>> ObtenerTodos(Expression<Func<T, bool>> filtro = null, string? incluirPropiedades = null)
        {
            IQueryable<T> query = dbSet;
            if (filtro != null)
            {
                query = query.Where(filtro);
            }
            if (incluirPropiedades != null)
            {
                foreach (var incluirPro in incluirPropiedades.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(incluirPro);
                }
            }
            return await query.ToListAsync();
        }

        public PagedList<T> ObtenerTodosPaginado(Parametros parametros, Expression<Func<T, bool>> filtro = null, string? incluirPropiedades = null)
        {
            IQueryable<T> query = dbSet;
            if (filtro != null)
            {
                query = query.Where(filtro);
            }
            if (incluirPropiedades != null)
            {
                foreach (var incluirPro in incluirPropiedades.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(incluirPro);
                }
            }
            return PagedList<T>.ToPagedList(query, parametros.PageNumber, parametros.PageSize);
        }

        public async Task Remover(T entidad)
        {
            dbSet.Remove(entidad);
            await Grabar();
        }
    }
}
