using WebPersonal_API.Modelos;

namespace WebPersonal_API.Repositorio.IRepositorio
{
    public interface IProvinciaRepositorio : IRepositorio<CProvin>
    {
        /// <summary>
        /// Actualiza los campos de la tabla C_provin (Provincia)
        /// </summary>
        /// <returns></returns>       
        Task<CProvin> Actualizar(CProvin entidad);

        /// <summary>
        /// Devuelve un nuevo código para el clasificador de Provincia
        /// </summary>
        /// <returns></returns>
        public string GetCodigoProvincia();
    }
}
