using WebPersonal_MVC.Models.Dto;

namespace WebPersonal_MVC.Services.IServices
{
    public interface ICategoriaCargoService
    {
        Task<T> ObtenerTodos<T>();

        Task<T> Obtener<T>(string codigo);

        Task<T> Crear<T>(CCatcarCreateDto dto);

        Task<T> Actualizar<T>(CCatcarCreateDto dto);

        Task<T> Remover<T>(string codigo);
    }
}
