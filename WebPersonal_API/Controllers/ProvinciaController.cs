using AutoMapper;
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

namespace WebPersonal_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProvinciaController : ControllerBase
    {
        private readonly ILogger<ProvinciaController> _logger;
        private readonly IProvinciaRepositorio _provinciaRepo;
        // Implementado AutoMapper
        private readonly IMapper _mapper;
        // Implementado API Response
        protected APIResponse _response;

        // Implementando - Logger Inyeccion de Dependencia
        public ProvinciaController(ILogger<ProvinciaController> logger, IProvinciaRepositorio provinciaRepo, IMapper mapper)
        {
            _logger = logger;
            _provinciaRepo = provinciaRepo;
            _mapper = mapper;
            _response = new();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        // Devuelve todos los registros de la tabla
        public async Task<ActionResult<APIResponse>> GetProvincias()
        {
            try
            {
                IEnumerable<CProvin> tablaList = await _provinciaRepo.ObtenerTodos();

                // Implementando API Respose
                _logger.LogInformation("GetProvincias: Obteniendo todas las Provincias");
                _response.Resultado = _mapper.Map<IEnumerable<CProvinDto>>(tablaList);
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

        [HttpGet("{codProvin}", Name = "GetProvincia")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        // Devuelve el registro de la tabla que se pase como parametro 
        public async Task<ActionResult<APIResponse>> GetProvincia(string codProvin)
        {
            try
            {
                var MenBadRequest = "En blanco o null el código de la provincia";
                var MenNotFound = "No existe la provincia con Id " + codProvin;
                if (string.IsNullOrEmpty(codProvin))
                {
                    _logger.LogError(MenBadRequest);
                    _response.IsExitoso = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.ErrorMessages = new List<string>() { MenBadRequest };
                    return BadRequest(_response);
                }

                var registro = await _provinciaRepo.Obtener(v => v.CodProvin == codProvin);
                if (registro == null)
                {
                    _logger.LogError(MenNotFound);
                    _response.IsExitoso = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.ErrorMessages = new List<string>() { MenNotFound };
                    return NotFound(_response);
                }

                _logger.LogInformation("GetProvincia: " + registro.CodProvin + " - " + registro.NomProvin);
                _response.Resultado = _mapper.Map<CProvinDto>(registro);
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
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        // Crea un nuevo registro en la tabla
        public async Task<ActionResult<APIResponse>> CrearProvincia([FromBody] CProvinCreateDto createDto)
        {
            try
            {
                var MenBadRequest = "La provincia con el nombre: [" + createDto.NomProvin + "] ya existe en la tabla!";
                // Implementando Validaciones ModelState
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                // Implementando Validaciones Personalizadas
                if (await _provinciaRepo.Obtener(v => v.NomProvin!.ToLower() == createDto.NomProvin!.ToLower()) != null)
                {
                    ModelState.AddModelError("ErrorMessages", "La categoría con ese Nombre ya existe!");
                    _logger.LogError("CrearProvincia: " + MenBadRequest);
                    //_response.IsExitoso = false;
                    //_response.StatusCode = HttpStatusCode.BadRequest;
                    //_response.ErrorMessages = new List<string>() { MenBadRequest };
                    return BadRequest(ModelState);
                }

                if (createDto == null)
                {
                    return BadRequest(createDto);
                }

                // Obteniendo un nuevo código de categoria de cargos
                createDto.CodProvin = _provinciaRepo.GetCodigoProvincia();
                // Funcionando AutoMapper
                CProvin modelo = _mapper.Map<CProvin>(createDto);

                // Insertando los datos en la tabla
                await _provinciaRepo.Crear(modelo);
                _response.Resultado = modelo;
                _response.StatusCode = HttpStatusCode.Created;

                _logger.LogInformation("CrearProvincia: Nueva Provincia: " + modelo.CodProvin + " - " + modelo.NomProvin);
                return CreatedAtRoute("GetProvincia", new { CodProvin = modelo.CodProvin }, _response);
            }
            catch (Exception ex)
            {
                _response.IsExitoso = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpDelete("{codProvin}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteProvincia(string codProvin)
        {
            try
            {
                var MenBadRequest = "En blanco o null el código de provincia";
                var MenNotFound = "No existe la provincia con Id " + codProvin;
                if (string.IsNullOrWhiteSpace(codProvin))
                {
                    _response.IsExitoso = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.ErrorMessages = new List<string>() { MenBadRequest };
                    return BadRequest(_response);
                }

                var registro = await _provinciaRepo.Obtener(v => v.CodProvin == codProvin);
                if (registro == null)
                {
                    _response.IsExitoso = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.ErrorMessages = new List<string>() { MenNotFound };
                    return NotFound(_response);
                }

                // Eliminando el registro
                await _provinciaRepo.Remover(registro);

                _logger.LogInformation("DeleteProvincia: " + registro.CodProvin + " - " + registro.NomProvin);
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

        [HttpPut("{codProvin}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        // Actualiza todos los registro de la tabla
        public async Task<IActionResult> UpdateProvincia(string codProvin, [FromBody] CProvinUpdateDto updateDto)
        {
            var MenBadRequest = "En blanco o null el código de provincia";
            if (updateDto == null || codProvin != updateDto.CodProvin)
            {
                _logger.LogError(MenBadRequest);
                _response.IsExitoso = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages = new List<string>() { MenBadRequest };
                return BadRequest(_response);
            }

            CProvin modelo = _mapper.Map<CProvin>(updateDto);

            // Actualizando los datos en la tabla
            await _provinciaRepo.Actualizar(modelo);
            _logger.LogInformation("UpdateProvincia: " + updateDto.CodProvin + " - " + updateDto.NomProvin);
            _response.StatusCode = HttpStatusCode.NoContent;
            return Ok(_response);
        }

        [HttpPatch("{codProvin}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        // Actualiza solo un campo de la tabla
        public async Task<IActionResult> UpdatePartialProvincia(string codProvin, JsonPatchDocument<CProvinUpdateDto> patchDto)
        {
            var MenBadRequest = "En blanco o null el código de provincia";
            var MenNotFound = "No existe la provincia con Id " + codProvin;
            if (patchDto == null || string.IsNullOrEmpty(codProvin))
            {
                _logger.LogError(MenBadRequest);
                _response.IsExitoso = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages = new List<string>() { MenBadRequest };
                return BadRequest(_response);
            }

            // Implementando AsNoTracking
            var registro = await _provinciaRepo.Obtener(v => v.CodProvin == codProvin, tracked:false);
            if (registro == null)
            {
                _response.IsExitoso = false;
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.ErrorMessages = new List<string>() { MenNotFound };
                return NotFound(_response);
            }

            CProvinUpdateDto cprovinDto = _mapper.Map<CProvinUpdateDto>(registro);

            patchDto.ApplyTo(cprovinDto, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); 
            }

            CProvin modelo = _mapper.Map<CProvin>(cprovinDto);

            // Actualizando los datos en la tabla
            await _provinciaRepo.Actualizar(modelo);

            _logger.LogInformation("UpdatePartialProvincia: " + cprovinDto.CodProvin + " - " + cprovinDto.NomProvin);
            _response.StatusCode = HttpStatusCode.NoContent;
            return Ok(_response);
        }
    }
}
