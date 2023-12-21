using WebPersonal_API.Modelos;

namespace WebPersonal_API.Repositorio.IRepositorio
{
    public interface ICategoriaCargoRepositorio : IRepositorio<C_catcar>
    {
        /// <summary>
        /// Actualiza los campos de la tabla C:catcar (Categoría de Cargos)
        /// </summary>
        /// <returns></returns>       
        Task<C_catcar> Actualizar(C_catcar entidad);

        /// <summary>
        /// Devuelve un nuevo código para el clasificador de Categoría de Cargos
        /// </summary>
        /// <returns></returns>
        public string GetCodigoCategoriaCargos();
    }
}
