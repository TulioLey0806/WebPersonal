using WebPersonal_MVC.Models.Dto;

namespace WebPersonal_MVC.Services.IServices
{
    public interface IProvinciaService
    {
        Task<T> ObtenerTodos<T>(string token);

        Task<T> ObtenerTodosPaginado<T>(string token, int pageNumber = 1, int pageSize = 5);

        Task<T> Obtener<T>(string codigo, string token);

        Task<T> Crear<T>(CProvinCreateDto dto, string token);

        Task<T> Actualizar<T>(CProvinUpdateDto dto, string token);

        Task<T> Remover<T>(string codigo, string token);        
    }
}
