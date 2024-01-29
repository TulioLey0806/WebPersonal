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

namespace WebPersonal_API.Controllers.v1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
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

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        // Crea un nuevo registro en la tabla
        public async Task<ActionResult<APIResponse>> CrearMunicipio([FromBody] CMuniciCreateDto createDto)
        {
            try
            {
                var MenBadRequest = "El municipio ya existe en la tabla!";
                // Implementando Validaciones ModelState
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                // Implementando Validaciones Personalizadas
                if (await _municipioRepo.Obtener(v => v.CodProvin == createDto.CodProvin && v.CodMunici == createDto.CodMunici) != null)
                {
                    ModelState.AddModelError("ErrorMessages", "El municipio ya existe!");
                    _logger.LogError("CrearMunicipio: " + MenBadRequest);
                    //_response.IsExitoso = false;
                    //_response.StatusCode = HttpStatusCode.BadRequest;
                    //_response.ErrorMessages = new List<string>() { MenBadRequest };
                    return BadRequest(ModelState);
                }
                if (await _provinciaRepo.Obtener(v => v.CodProvin == createDto.CodProvin) == null)
                {
                    ModelState.AddModelError("ErrorMessages", "La provincia no existe!");
                    _logger.LogError("CrearMunicipio: " + MenBadRequest);
                    //_response.IsExitoso = false;
                    //_response.StatusCode = HttpStatusCode.BadRequest;
                    //_response.ErrorMessages = new List<string>() { MenBadRequest };
                    return BadRequest(ModelState);
                }

                if (createDto == null)
                {
                    return BadRequest(createDto);
                }

                // Obteniendo un nuevo código del municipio
                createDto.CodMunici = _municipioRepo.GetCodigoMunicipio(createDto.CodProvin);
                // Funcionando AutoMapper
                CMunici modelo = _mapper.Map<CMunici>(createDto);

                // Insertando los datos en la tabla
                await _municipioRepo.Crear(modelo);
                _response.Resultado = modelo;
                _response.StatusCode = HttpStatusCode.Created;

                _logger.LogInformation("CrearMunicipio: Nuevo Municipio: " + modelo.CodMunici + " - " + modelo.NomMunici);
                return CreatedAtRoute("GetMunicipio", new { modelo.CodProvin, modelo.CodMunici }, _response);
            }
            catch (Exception ex)
            {
                _response.IsExitoso = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpDelete("{codProvin},{codMunici}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteMunicipio(string codProvin, string codMunici)
        {
            try
            {
                var MenBadRequest = "En blanco o null el código de municipio";
                var MenNotFound = "No existe el municipio";
                if (string.IsNullOrWhiteSpace(codProvin) || string.IsNullOrWhiteSpace(codMunici))
                {
                    _response.IsExitoso = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.ErrorMessages = new List<string>() { MenBadRequest };
                    return BadRequest(_response);
                }

                var registro = await _municipioRepo.Obtener(v => v.CodProvin == codProvin && v.CodMunici == codMunici);
                if (registro == null)
                {
                    _response.IsExitoso = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.ErrorMessages = new List<string>() { MenNotFound };
                    return NotFound(_response);
                }

                // Eliminando el registro
                await _municipioRepo.Remover(registro);

                _logger.LogInformation("DeleteMunicipio: " + registro.CodMunici + " - " + registro.NomMunici);
                _response.StatusCode = HttpStatusCode.NoContent;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsExitoso = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return BadRequest(_response);
        }

        [HttpPut("{codProvin},{codMunici}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        // Actualiza todos los registro de la tabla
        public async Task<IActionResult> UpdateMunicipio(string codProvin, string codMunici, [FromBody] CMuniciUpdateDto updateDto)
        {
            var MenBadRequest = "En blanco o null el código de municipio";
            var MenNotFound = "No existe el municipio con Id " + codProvin + "-" + codMunici;
            if (updateDto == null || codProvin != updateDto.CodProvin || codMunici != updateDto.CodMunici)
            {
                _logger.LogError(MenBadRequest);
                _response.IsExitoso = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages = new List<string>() { MenBadRequest };
                return BadRequest(_response);
            }

            if (await _provinciaRepo.Obtener(v => v.CodProvin == updateDto.CodProvin) == null)
            {
                ModelState.AddModelError("RegistroNoExiste", "La provincia no existe!");
                _logger.LogError("UpdateProvincia: " + MenBadRequest);
                //_response.IsExitoso = false;
                //_response.StatusCode = HttpStatusCode.BadRequest;
                //_response.ErrorMessages = new List<string>() { MenBadRequest };
                return BadRequest(ModelState);
            }

            if (await _municipioRepo.Obtener(v => v.CodProvin == codProvin && v.CodMunici == codMunici) == null)
            {
                _logger.LogError(MenNotFound);
                _response.IsExitoso = false;
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.ErrorMessages = new List<string>() { MenNotFound };
                return NotFound(_response);
            }

            CMunici modelo = _mapper.Map<CMunici>(updateDto);

            // Actualizando los datos en la tabla
            await _municipioRepo.Actualizar(modelo);
            _logger.LogInformation("UpdateProvincia: " + updateDto.CodMunici + " - " + updateDto.NomMunici);
            _response.StatusCode = HttpStatusCode.NoContent;
            return Ok(_response);
        }

        [HttpPatch("{codProvin},{codMunici}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        // Actualiza solo un campo de la tabla
        public async Task<IActionResult> UpdatePartialMunicipio(string codProvin, string codMunici, JsonPatchDocument<CMuniciUpdateDto> patchDto)
        {
            var MenBadRequest = "En blanco o null el código de provincia";
            var MenNotFound = "No existe el municipio con Id " + codProvin + "-" + codMunici;
            if (patchDto == null || string.IsNullOrEmpty(codProvin) || string.IsNullOrEmpty(codMunici))
            {
                _logger.LogError(MenBadRequest);
                _response.IsExitoso = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages = new List<string>() { MenBadRequest };
                return BadRequest(_response);
            }

            // Implementando AsNoTracking
            // Validando que exista el codigo de provincia
            var registroProvin = await _provinciaRepo.Obtener(v => v.CodProvin == codProvin, tracked: false);
            if (registroProvin == null)
            {
                _response.IsExitoso = false;
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.ErrorMessages = new List<string>() { MenNotFound };
                return NotFound(_response);
            }

            var registro = await _municipioRepo.Obtener(v => v.CodProvin == codProvin && v.CodMunici == codMunici);
            if (registro == null)
            {
                _response.IsExitoso = false;
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.ErrorMessages = new List<string>() { MenNotFound };
                return NotFound(_response);
            }

            var cmuniciDto = _mapper.Map<CMuniciUpdateDto>(registro);

            patchDto.ApplyTo(cmuniciDto, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var modelo = _mapper.Map<CMunici>(cmuniciDto);

            // Actualizando los datos en la tabla
            await _municipioRepo.Actualizar(modelo);

            _logger.LogInformation("UpdatePartialMunicipio: " + cmuniciDto.CodMunici + " - " + cmuniciDto.NomMunici);
            _response.StatusCode = HttpStatusCode.NoContent;
            return Ok(_response);
        }
    }
}
