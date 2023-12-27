using WebPersonal_API.Modelos;

namespace WebPersonal_API.Repositorio.IRepositorio
{
    public interface ICategoriaCargoRepositorio : IRepositorio<CCatcar>
    {
        /// <summary>
        /// Actualiza los campos de la tabla C:catcar (Categoría de Cargos)
        /// </summary>
        /// <returns></returns>       
        Task<CCatcar> Actualizar(CCatcar entidad);

        /// <summary>
        /// Devuelve un nuevo código para el clasificador de Categoría de Cargos
        /// </summary>
        /// <returns></returns>
        public string GetCodigoCategoriaCargos();
    }
}
