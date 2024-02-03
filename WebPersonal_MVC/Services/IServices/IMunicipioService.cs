using WebPersonal_MVC.Models.Dto;

namespace WebPersonal_MVC.Services.IServices
{
    public interface IMunicipioService
    {
        Task<T> ObtenerTodos<T>(string token);

        Task<T> ObtenerTodosPaginado<T>(string token, int pageNumber = 1, int pageSize = 5);

        Task<T> Obtener<T>(string codProvin, string codMunici, string token);

        Task<T> Crear<T>(CMuniciCreateDto dto, string token);

        Task<T> Actualizar<T>(CMuniciUpdateDto dto, string token);

        Task<T> Remover<T>(string codProvin, string codMunici, string token);
    }
}
