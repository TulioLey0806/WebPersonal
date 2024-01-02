using WebPersonal_MVC.Models.Dto;

namespace WebPersonal_MVC.Services.IServices
{
    public interface IMunicipioService
    {
        Task<T> ObtenerTodos<T>();

        Task<T> Obtener<T>(string codProvin, string codMunici);

        Task<T> Crear<T>(CMuniciCreateDto dto);

        Task<T> Actualizar<T>(CMuniciUpdateDto dto);

        Task<T> Remover<T>(string codProvin, string codMunici);
    }
}
