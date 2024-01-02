using WebPersonal_MVC.Models.Dto;

namespace WebPersonal_MVC.Services.IServices
{
    public interface IProvinciaService
    {
        Task<T> ObtenerTodos<T>();

        Task<T> Obtener<T>(string codigo);

        Task<T> Crear<T>(CProvinCreateDto dto);

        Task<T> Actualizar<T>(CProvinUpdateDto dto);

        Task<T> Remover<T>(string codigo);        
    }
}
