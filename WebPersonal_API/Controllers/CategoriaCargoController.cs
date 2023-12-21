using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebPersonal_API.Datos;
using WebPersonal_API.Modelos;
using WebPersonal_API.Modelos.Dto;
using WebPersonal_API.Repositorio.IRepositorio;

namespace WebPersonal_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaCargoController : ControllerBase
    {
        private readonly ILogger<CategoriaCargoController> _logger;
        private readonly ICategoriaCargoRepositorio _categoriacargoRepo;
        private readonly IMapper _mapper;

        // Implementando - Logger Inyeccion de Dependencia
        public CategoriaCargoController(ILogger<CategoriaCargoController> logger, ICategoriaCargoRepositorio categoriacargoRepo, IMapper mapper)
        {
            _logger = logger;
            _categoriacargoRepo = categoriacargoRepo;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<C_catcarDto>>> GetCategoriaCargos()
        {
            IEnumerable<C_catcar> c_catcarList = await _categoriacargoRepo.ObtenerTodos();

            _logger.LogInformation("GetCategoriaCargos: Obteniendo todas las Categorias de Cargos");
            return Ok(_mapper.Map<IEnumerable<C_catcarDto>>(c_catcarList));
        }

        [HttpGet("cod_catcar:string", Name = "GetCategoriaCargo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<C_catcarDto>> GetCategoriaCargo(string cod_catcar)
        {
            if (string.IsNullOrEmpty(cod_catcar))
            {
                _logger.LogError("Error obteniendo la categoria de cargos con Id " + cod_catcar);
                return BadRequest();
            }

            //var catgoriaCargo = C_catcarStore.c_catcarList.FirstOrDefault(v => v.Cod_catcar == cod_catcar);
            var catgoriaCargo = await _categoriacargoRepo.Obtener(v => v.Cod_catcar == cod_catcar);
             
            if (catgoriaCargo == null)
            {
                _logger.LogError("No existe la categoria de cargos con Id " + cod_catcar);
                return NotFound();
            }

            _logger.LogInformation("GetCategoriaCargo: " + catgoriaCargo.Cod_catcar + " - " + catgoriaCargo.Nom_catcar);
            return Ok(_mapper.Map<C_catcarDto>(catgoriaCargo));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<C_catcarDto>> CrearCategoriaCargo([FromBody] C_catcarCreateDto createDto)
        {
            // Implementando Validaciones ModelState
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // Implementando Validaciones Personalizadas
            if(await _categoriacargoRepo.Obtener(v => v.Nom_catcar!.ToLower() == createDto.Nom_catcar!.ToLower()) != null)
            {
                ModelState.AddModelError("NombreExiste", "La categoría con ese Nombre ya existe!");
                return BadRequest(ModelState);
            }

            if(createDto == null)
            {
                return BadRequest(createDto);
            } 
            //if (string.IsNullOrEmpty(c_catcarDto.Cod_catcar))
            //{
            //    return StatusCode(StatusCodes.Status500InternalServerError);
            //}

            // Obteniendo un nuevo código de categoria de cargos
            createDto.Cod_catcar = _categoriacargoRepo.GetCodigoCategoriaCargos();

            //C_catcar modelo = new() 
            //{
            //    Cod_catcar = createDto.Cod_catcar,
            //    Nom_catcar = createDto.Nom_catcar
            //};

            C_catcar modelo = _mapper.Map<C_catcar>(createDto);

            // Insertando los datos en la tabla
            await _categoriacargoRepo.Crear(modelo);

            _logger.LogInformation("CrearCategoriaCargo: " + modelo.Cod_catcar + " - " + modelo.Nom_catcar);
            return CreatedAtRoute("GetCategoriaCargo", new {cod_catcar = modelo.Cod_catcar}, modelo);
        }

        [HttpDelete("cod_catcar:string")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteCategoriaCargo(string cod_catcar) 
        {
            if (string.IsNullOrWhiteSpace(cod_catcar))
            {
                return BadRequest();
            }

            var categoriaCargo = await _categoriacargoRepo.Obtener(v => v.Cod_catcar == cod_catcar);
            if (categoriaCargo == null)
            {
                return NotFound();
            }

            // Eliminando la categoria de cargo
            await _categoriacargoRepo.Remover(categoriaCargo);

            _logger.LogInformation("DeleteCategoriaCargo: " + categoriaCargo.Cod_catcar + " - " + categoriaCargo.Nom_catcar);
            return NoContent();
        }  

        [HttpPut("cod_catcar:string")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateCategoriaCargo(string cod_catcar, [FromBody] C_catcarUpdateDto updateDto)
        {
           if(updateDto == null || cod_catcar != updateDto.Cod_catcar)
            {
                return BadRequest();
            }

            //var categoriaCargo = C_catcarStore.c_catcarList.FirstOrDefault(v => v.Cod_catcar == cod_catcar);
            //categoriaCargo!.Nom_catcar = c_catcarDto.Nom_catcar;

            //C_catcar modelo = new()
            //{
            //    Cod_catcar = updateDto.Cod_catcar,                
            //    Nom_catcar = updateDto.Nom_catcar
            //};

            C_catcar modelo = _mapper.Map<C_catcar>(updateDto);

            // Actualizando los datos en la tabla
            await _categoriacargoRepo.Actualizar(modelo);

            _logger.LogInformation("UpdateCategoriaCargo: " + updateDto.Cod_catcar + " - " + updateDto.Nom_catcar);
            return NoContent();
        }

        [HttpPatch("cod_catcar:string")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePartialCategoriaCargo(string cod_catcar, JsonPatchDocument<C_catcarUpdateDto> pathDto)
        {
            if (pathDto == null || string.IsNullOrEmpty(cod_catcar))
            {
                return BadRequest();
            }

            //var categoria = C_catcarStore.c_catcarList.FirstOrDefault(v => v.Cod_catcar == cod_catcar);
            // Implementando AsNoTracking
            var categoria = await _categoriacargoRepo.Obtener(v => v.Cod_catcar == cod_catcar, tracked:false);

            //C_catcarUpdateDto c_catcarDto = new()
            //{
            //    Cod_catcar = categoria.Cod_catcar,
            //    Nom_catcar = categoria.Nom_catcar
            //};

            C_catcarUpdateDto c_catcarDto = _mapper.Map<C_catcarUpdateDto>(categoria);

            if (categoria == null) return BadRequest();

            pathDto.ApplyTo(c_catcarDto, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); 
            }

            //C_catcar modelo = new()
            //{
            //    Cod_catcar = c_catcarDto.Cod_catcar,
            //    Nom_catcar = c_catcarDto.Nom_catcar
            //};

            C_catcar modelo = _mapper.Map<C_catcar>(c_catcarDto);

            // Actualizando los datos en la tabla
            await _categoriacargoRepo.Actualizar(modelo);

            _logger.LogInformation("UpdatePartialCategoriaCargo: " + c_catcarDto.Cod_catcar + " - " + c_catcarDto.Nom_catcar);
            return NoContent();
        }
    }
}
