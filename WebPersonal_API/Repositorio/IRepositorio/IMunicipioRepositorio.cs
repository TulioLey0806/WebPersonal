using WebPersonal_API.Modelos;

namespace WebPersonal_API.Repositorio.IRepositorio
{
    public interface IMunicipioRepositorio : IRepositorio<CMunici>
    {
        /// <summary>
        /// Actualiza los campos de la tabla C_munici (Municipios)
        /// </summary>
        /// <returns></returns>       
        Task<CMunici> Actualizar(CMunici entidad);

        /// <summary>
        /// Devuelve un nuevo código para el clasificador de Municipios
        /// </summary>
        /// <param name="codprovin">Código de provincia</param>
        /// <returns></returns>
        public string GetCodigoMunicipio(string codprovin);
    }
}
