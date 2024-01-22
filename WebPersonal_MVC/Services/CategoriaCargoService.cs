using Microsoft.IdentityModel.Tokens;
using WebPersonal_MVC.Models;
using WebPersonal_MVC.Models.Dto;
using WebPersonal_MVC.Services.IServices;
using WebPersonal_Utilidad;

namespace WebPersonal_MVC.Services
{
    public class CategoriaCargoService : BaseService, ICategoriaCargoService
    {
        public readonly IHttpClientFactory _httpClient;
        private string _apiUrl;

        public CategoriaCargoService(IHttpClientFactory httpClient, IConfiguration configuration) : base(httpClient)
        {
            _httpClient = httpClient;
            _apiUrl = configuration.GetValue<string>("ServiceUrls:API_URL");
        }

        public Task<T> Actualizar<T>(CCatcarCreateDto dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                APITipo = DS.APITipo.PUT,
                Datos = dto,
                Url = _apiUrl + "/api/CategoriaCargo/" + dto.CodCatcar,
                Token = token
            });
        }

        public Task<T> Crear<T>(CCatcarCreateDto dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                APITipo = DS.APITipo.POST,
                Datos = dto,
                Url = _apiUrl + "/api/CategoriaCargo/",
                Token = token
            });
        }

        public Task<T> Obtener<T>(string codigo, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                APITipo = DS.APITipo.GET,
                Url = _apiUrl + "/api/CategoriaCargo/" + codigo,
                Token = token
            });
        }

        public Task<T> ObtenerTodos<T>(string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                APITipo = DS.APITipo.GET,
                Url = _apiUrl + "/api/CategoriaCargo/",
                Token = token
            });
        }

        public Task<T> Remover<T>(string codigo, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                APITipo = DS.APITipo.DELETE,
                Url = _apiUrl + "/api/CategoriaCargo/" + codigo,
                Token = token
            }); 
        }
    }
}
