using System.Linq.Expressions;

namespace WebPersonal_API.Repositorio.IRepositorio
{
    public interface IRepositorio<T> where T : class
    {
        /// <summary>
        /// Crea un nuevo registro para la entidad 
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        Task Crear(T entidad);

        /// <summary>
        /// Devuelve todos los registros de la entidad
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        Task<List<T>> ObtenerTodos(Expression<Func<T, bool>> filtro = null);

        /// <summary>
        /// Devuelve un solo registro de la entidad
        /// </summary>
        /// <param name="filtro"></param>
        /// <param name="tracked"></param>
        /// <returns></returns>
        Task<T> Obtener(Expression<Func<T, bool>> filtro = null, bool tracked = true);

        /// <summary>
        /// Elimina un registro de la entidad
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        Task Remover(T entidad);

        /// <summary>
        /// Salva los cambios en la entidad
        /// </summary>
        /// <returns></returns>
        Task Grabar();
    }
}
