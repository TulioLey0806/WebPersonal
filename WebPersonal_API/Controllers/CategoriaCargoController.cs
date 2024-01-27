using Asp.Versioning;
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
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class CategoriaCargoController : ControllerBase
    {
        private readonly ILogger<CategoriaCargoController> _logger;
        private readonly ICategoriaCargoRepositorio _categoriacargoRepo;
        // Implemenyado AutoMapper
        private readonly IMapper _mapper;
        // Implementado API Response
        protected APIResponse _response;

        // Implementando - Logger Inyeccion de Dependencia
        public CategoriaCargoController(ILogger<CategoriaCargoController> logger, ICategoriaCargoRepositorio categoriacargoRepo, IMapper mapper)
        {
            _logger = logger;
            _categoriacargoRepo = categoriacargoRepo;
            _mapper = mapper;
            _response = new();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        // Devuelve todos los registros de la tabla
        public async Task<ActionResult<APIResponse>> GetCategoriaCargos()
        {
            try
            {
                _logger.LogInformation("GetCategoriaCargos: Obteniendo todas las Categorias de Cargos");

                IEnumerable<CCatcar> tablaList = await _categoriacargoRepo.ObtenerTodos();

                // Implementando API Respose
                _response.Resultado = _mapper.Map<IEnumerable<CCatcarDto>>(tablaList);
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

        [HttpGet("{codigo}", Name = "GetCategoriaCargo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        // Devuelve el registro de la tabla que se pase como parametro 
        public async Task<ActionResult<APIResponse>> GetCategoriaCargo(string codigo)
        {
            try
            {
                var MenBadRequest = "En blanco o null la categoria de cargos";
                var MenNotFound = "No existe la categoria de cargos con Id " + codigo;
                if (string.IsNullOrEmpty(codigo))
                {
                    _logger.LogError(MenBadRequest);
                    _response.IsExitoso = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.ErrorMessages = new List<string>() { MenBadRequest };
                    return BadRequest(_response);
                }

                var registro = await _categoriacargoRepo.Obtener(v => v.CodCatcar == codigo);
                if (registro == null)
                {
                    _logger.LogError(MenNotFound);
                    _response.IsExitoso = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.ErrorMessages = new List<string>() { MenNotFound };
                    return NotFound(_response);
                }

                _logger.LogInformation("GetCategoriaCargo: " + registro.CodCatcar + " - " + registro.NomCatcar);
                _response.Resultado = _mapper.Map<CCatcarDto>(registro);
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
        public async Task<ActionResult<APIResponse>> CrearCategoriaCargo([FromBody] CCatcarCreateDto createDto)
        {
            try
            {
                var MenBadRequest = "La categoria con el nombre: [" + createDto.NomCatcar + "] ya existe en la tabla!";
                // Implementando Validaciones ModelState
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                // Implementando Validaciones Personalizadas
                if (await _categoriacargoRepo.Obtener(v => v.NomCatcar!.ToLower() == createDto.NomCatcar!.ToLower()) != null)
                {
                    //ModelState.AddModelError("NombreExiste", "La categoría con ese Nombre ya existe!");
                    _logger.LogError("CrearCategoriaCargo: " + MenBadRequest);
                    _response.IsExitoso = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.ErrorMessages = new List<string>() { MenBadRequest };
                    return BadRequest(_response);
                }

                if (createDto == null)
                {
                    return BadRequest(createDto);
                }

                // Obteniendo un nuevo código de categoria de cargos
                createDto.CodCatcar = _categoriacargoRepo.GetCodigoCategoriaCargos();
                // Funcionando AutoMapper
                CCatcar modelo = _mapper.Map<CCatcar>(createDto);

                // Insertando los datos en la tabla
                await _categoriacargoRepo.Crear(modelo);
                _response.Resultado = modelo;
                _response.StatusCode = HttpStatusCode.Created;

                _logger.LogInformation("CrearCategoriaCargo: Nueva Categoria de cargo: " + modelo.CodCatcar + " - " + modelo.NomCatcar);
                return CreatedAtRoute("GetCategoriaCargo", new { CodCatcar = modelo.CodCatcar }, _response);
            }
            catch (Exception ex)
            {
                _response.IsExitoso = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpDelete("{codigo}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteCategoriaCargo(string codigo) 
        {
            try
            {
                var MenBadRequest = "En blanco o null la categoria de cargos";
                var MenNotFound = "No existe la categoria de cargos con Id " + codigo;
                if (string.IsNullOrWhiteSpace(codigo))
                {
                    _response.IsExitoso = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.ErrorMessages = new List<string>() { MenBadRequest };
                    return BadRequest(_response);
                }

                var registro = await _categoriacargoRepo.Obtener(v => v.CodCatcar == codigo);
                if (registro == null)
                {
                    _response.IsExitoso = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.ErrorMessages = new List<string>() { MenNotFound };
                    return NotFound(_response);
                }

                // Eliminando la categoria de cargo
                await _categoriacargoRepo.Remover(registro);

                _logger.LogInformation("DeleteCategoriaCargo: " + registro.CodCatcar + " - " + registro.NomCatcar);
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

        [HttpPut("{codigo}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        // Actualiza todos los registro de la tabla
        public async Task<IActionResult> UpdateCategoriaCargo(string codigo, [FromBody] CCatcarUpdateDto updateDto)
        {
            var MenBadRequest = "En blanco o null la categoria de cargos";
            if (updateDto == null || codigo != updateDto.CodCatcar)
            {
                _logger.LogError(MenBadRequest);
                _response.IsExitoso = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages = new List<string>() { MenBadRequest };
                return BadRequest(_response);
            }

            CCatcar modelo = _mapper.Map<CCatcar>(updateDto);

            // Actualizando los datos en la tabla
            await _categoriacargoRepo.Actualizar(modelo);
            _logger.LogInformation("UpdateCategoriaCargo: " + updateDto.CodCatcar + " - " + updateDto.NomCatcar);
            _response.StatusCode = HttpStatusCode.NoContent;
            return Ok(_response);
        }

        [HttpPatch("{codigo}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        // Actualiza solo un campo de la tabla
        public async Task<IActionResult> UpdatePartialCategoriaCargo(string codigo, JsonPatchDocument<CCatcarUpdateDto> updateDto)
        {
            var MenBadRequest = "En blanco o null la categoria de cargos";
            var MenNotFound = "No existe la categoria de cargos con Id " + codigo;
            if (updateDto == null || string.IsNullOrEmpty(codigo))
            {
                _logger.LogError(MenBadRequest);
                _response.IsExitoso = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages = new List<string>() { MenBadRequest };
                return BadRequest(_response);
            }

            // Implementando AsNoTracking
            var registro = await _categoriacargoRepo.Obtener(v => v.CodCatcar == codigo, tracked:false);
            if (registro == null)
            {
                _response.IsExitoso = false;
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.ErrorMessages = new List<string>() { MenNotFound };
                return NotFound(_response);
            }

            CCatcarUpdateDto ccatcarDto = _mapper.Map<CCatcarUpdateDto>(registro);

            updateDto.ApplyTo(ccatcarDto, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); 
            }

            CCatcar modelo = _mapper.Map<CCatcar>(ccatcarDto);

            // Actualizando los datos en la tabla
            await _categoriacargoRepo.Actualizar(modelo);

            _logger.LogInformation("UpdatePartialCategoriaCargo: " + ccatcarDto.CodCatcar + " - " + ccatcarDto.NomCatcar);
            _response.StatusCode = HttpStatusCode.NoContent;
            return Ok(_response);
        }
    }
}
