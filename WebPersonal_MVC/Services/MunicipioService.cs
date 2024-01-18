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

        public Task<T> Actualizar<T>(CMuniciUpdateDto dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                APITipo = DS.APITipo.PUT,
                Datos = dto,
                Url = _municipioUrl + "/api/Municipio/" + dto.CodProvin + ", " + dto.CodMunici
            });
        }

        public Task<T> Crear<T>(CMuniciCreateDto dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                APITipo = DS.APITipo.POST,
                Datos = dto,
                Url = _municipioUrl + "/api/Municipio/"
            });
        }

        public Task<T> Obtener<T>(string codProvin, string codMunici)
        {
            return SendAsync<T>(new APIRequest()
            {
                APITipo = DS.APITipo.GET,
                Url = _municipioUrl + "/api/Municipio/" + codProvin +","+ codMunici
            });
        }

        public Task<T> ObtenerTodos<T>()
        {
            return SendAsync<T>(new APIRequest()
            {
                APITipo = DS.APITipo.GET,
                Url = _municipioUrl + "/api/Municipio/"
            });
        }

        public Task<T> Remover<T>(string codProvin, string codMunici)
        {
            return SendAsync<T>(new APIRequest()
            {
                APITipo = DS.APITipo.DELETE,
                Url = _municipioUrl + "/api/Municipio/" + codProvin + "," + codMunici
            });
        }
    }
}
