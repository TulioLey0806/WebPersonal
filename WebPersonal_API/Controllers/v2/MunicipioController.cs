using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using WebPersonal_API.Datos;
using WebPersonal_API.Modelos;
using WebPersonal_API.Modelos.Dto;
using WebPersonal_API.Repositorio.IRepositorio;

namespace WebPersonal_API.Controllers.v2
{
    //[Route("api/[controller]")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("2.0")]
    public class MunicipioController : ControllerBase
    {
        private readonly ILogger<MunicipioController> _logger;
        private readonly IProvinciaRepositorio _provinciaRepo;
        private readonly IMunicipioRepositorio _municipioRepo;
        // Implemenyado AutoMapper
        private readonly IMapper _mapper;
        // Implementado API Response
        protected APIResponse _response;

        // Implementando - Logger Inyeccion de Dependencia
        public MunicipioController(ILogger<MunicipioController> logger,
                                   IProvinciaRepositorio provinciaRepo,
                                   IMunicipioRepositorio municipioRepo,
                                   IMapper mapper)
        {
            _logger = logger;
            _provinciaRepo = provinciaRepo;
            _municipioRepo = municipioRepo;
            _mapper = mapper;
            _response = new();
        }

        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        // Devuelve todos los registros de la tabla
        public async Task<ActionResult<APIResponse>> GetMunicipios()
        {
            try
            {
                IEnumerable<CMunici> tablaList = await _municipioRepo.ObtenerTodos(incluirPropiedades: "CodProvinNavigation");

                // Implementando API Respose
                _logger.LogInformation("GetMunicipios: Obteniendo todos los Municipios");
                _response.Resultado = _mapper.Map<IEnumerable<CMuniciDto>>(tablaList);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsExitoso = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpGet("{codProvin},{codMunici}", Name = "GetMunicipio")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        // Devuelve el registro de la tabla que se pase como parametro 
        public async Task<ActionResult<APIResponse>> GetMunicipio(string codProvin, string codMunici)
        {
            try
            {
                var MenBadRequest = "En blanco o null el código de municipio";
                var MenNotFound = "No existe el municipio con Id " + codProvin + "-" + codMunici;
                if (string.IsNullOrEmpty(codProvin) || string.IsNullOrEmpty(codMunici))
                {
                    _logger.LogError(MenBadRequest);
                    _response.IsExitoso = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.ErrorMessages = new List<string>() { MenBadRequest };
                    return BadRequest(_response);
                }

                var registro = await _municipioRepo.Obtener(v => v.CodProvin == codProvin && v.CodMunici == codMunici, incluirPropiedades: "CodProvinNavigation");
                if (registro == null)
                {
                    _logger.LogError(MenNotFound);
                    _response.IsExitoso = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.ErrorMessages = new List<string>() { MenNotFound };
                    return NotFound(_response);
                }

                _logger.LogInformation("GetMunicipio: " + registro.CodMunici + " - " + registro.NomMunici);
                _response.Resultado = _mapper.Map<CMuniciDto>(registro);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsExitoso = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }
    }
}
