﻿using WebPersonal_API.Modelos;
using WebPersonal_MVC.Models;
using WebPersonal_MVC.Models.Dto;
using WebPersonal_MVC.Services.IServices;
using WebPersonal_Utilidad;

namespace WebPersonal_MVC.Services
{
    public class ProvinciaService : BaseService, IProvinciaService
    {
        public readonly IHttpClientFactory _httpClient;
        private string _provinciaUrl;

        public ProvinciaService(IHttpClientFactory httpClient, IConfiguration configuration) : base(httpClient)
        {
            _httpClient = httpClient;
            _provinciaUrl = configuration.GetValue<string>("ServiceUrls:API_URL");
        }

        public Task<T> Actualizar<T>(CProvinUpdateDto dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                APITipo = DS.APITipo.PUT,
                Datos = dto,
                Url = _provinciaUrl + "/api/v1/Provincia/" + dto.CodProvin,
                Token = token
            });
        }

        public Task<T> Crear<T>(CProvinCreateDto dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                APITipo = DS.APITipo.POST,
                Datos = dto,
                Url = _provinciaUrl + "/api/v1/Provincia/",
                Token = token
            });
        }

        public Task<T> Obtener<T>(string codigo, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                APITipo = DS.APITipo.GET,
                Url = _provinciaUrl + "/api/v1/Provincia/" + codigo,
                Token = token
            });
        }

        public Task<T> ObtenerTodos<T>(string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                APITipo = DS.APITipo.GET,
                Url = _provinciaUrl + "/api/v1/Provincia/",
                Token = token
            });
        }

        public Task<T> ObtenerTodosPaginado<T>(string token, int pageNumber = 1, int pageSize = 5)
        {
            return SendAsync<T>(new APIRequest()
            {
                APITipo = DS.APITipo.GET,
                Url = _provinciaUrl + "/api/v1/Provincia/ProvinciasPaginado",
                Token = token,
                Parametros = new Parametros() { PageNumber = pageNumber, PageSize = pageSize }
            });
        }

        public Task<T> Remover<T>(string codigo, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                APITipo = DS.APITipo.DELETE,
                Url = _provinciaUrl + "/api/v1/Provincia/" + codigo,
                Token = token
            });
        }
    }
}
