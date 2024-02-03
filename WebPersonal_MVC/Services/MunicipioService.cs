using WebPersonal_MVC.Models;
using WebPersonal_MVC.Models.Dto;
using WebPersonal_MVC.Services.IServices;
using WebPersonal_Utilidad;

namespace WebPersonal_MVC.Services
{
    public class MunicipioService :BaseService, IMunicipioService
    {
        public readonly IHttpClientFactory _httpClient;
        private string _municipioUrl;

        public MunicipioService(IHttpClientFactory httpClient, IConfiguration configuration) :base(httpClient)
        {
            _httpClient = httpClient;
            _municipioUrl = configuration.GetValue<string>("ServiceUrls:API_URL");
        }

        public Task<T> Actualizar<T>(CMuniciUpdateDto dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                APITipo = DS.APITipo.PUT,
                Datos = dto,
                Url = _municipioUrl + "/api/v1/Municipio/" + dto.CodProvin + "," + dto.CodMunici,
                Token = token
            });
        }

        public Task<T> Crear<T>(CMuniciCreateDto dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                APITipo = DS.APITipo.POST,
                Datos = dto,
                Url = _municipioUrl + "/api/v1/Municipio/",
                Token = token
            });
        }

        public Task<T> Obtener<T>(string codProvin, string codMunici, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                APITipo = DS.APITipo.GET,
                Url = _municipioUrl + "/api/v1/Municipio/" + codProvin +","+ codMunici,
                Token = token
            });
        }

        public Task<T> ObtenerTodos<T>(string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                APITipo = DS.APITipo.GET,
                Url = _municipioUrl + "/api/v1/Municipio/",
                Token = token
            });
        }

        public Task<T> ObtenerTodosPaginado<T>(string token, int pageNumber = 1, int pageSize = 5)
        {
            return SendAsync<T>(new APIRequest()
            {
                APITipo = DS.APITipo.GET,
                Url = _municipioUrl + "/api/v1/Municipio/MunicipiosPaginado",
                Token = token,
                Parametros = new Parametros() { PageNumber = pageNumber, PageSize = pageSize }
            });
        }

        public Task<T> Remover<T>(string codProvin, string codMunici, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                APITipo = DS.APITipo.DELETE,
                Url = _municipioUrl + "/api/v1/Municipio/" + codProvin + "," + codMunici,
                Token = token
            });
        }
    }
}
