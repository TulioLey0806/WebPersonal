using WebPersonal_API.Modelos;
using WebPersonal_MVC.Models;
using WebPersonal_MVC.Models.Dto;
using WebPersonal_MVC.Services.IServices;
using WebPersonal_Utilidad;

namespace WebPersonal_MVC.Services
{
    public class ProvinciaService : BaseService, IProvinciaService
    {
        public readonly IHttpClientFactory _httpClient;
        private string _apiUrl;

        public ProvinciaService(IHttpClientFactory httpClient, IConfiguration configuration) : base(httpClient)
        {
            _httpClient = httpClient;
            _apiUrl = configuration.GetValue<string>("ServiceUrls:API_URL");
        }

        public Task<T> Actualizar<T>(CProvinUpdateDto dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                APITipo = DS.APITipo.PUT,
                Datos = dto,
                Url = _apiUrl + "/api/Provincia/" + dto.CodProvin
            });
        }

        public Task<T> Crear<T>(CProvinCreateDto dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                APITipo = DS.APITipo.POST,
                Datos = dto,
                Url = _apiUrl + "/api/Provincia"
            });
        }

        public Task<T> Obtener<T>(string codigo)
        {
            return SendAsync<T>(new APIRequest()
            {
                APITipo = DS.APITipo.GET,
                Url = _apiUrl + "/api/Provincia/" + codigo
            });
        }

        public Task<T> ObtenerTodos<T>()
        {
            return SendAsync<T>(new APIRequest()
            {
                APITipo = DS.APITipo.GET,
                Url = _apiUrl + "/api/Provincia"
            });
        }

        public Task<T> Remover<T>(string codigo)
        {
            return SendAsync<T>(new APIRequest()
            {
                APITipo = DS.APITipo.DELETE,
                Url = _apiUrl + "/api/Provincia/" + codigo
            });
        }
    }
}
