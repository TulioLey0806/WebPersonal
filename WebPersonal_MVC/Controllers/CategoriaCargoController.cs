using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebPersonal_MVC.Models;
using WebPersonal_MVC.Models.Dto;
using WebPersonal_MVC.Services;
using WebPersonal_MVC.Services.IServices;

namespace WebPersonal_MVC.Controllers
{
    public class CategoriaCargoController : Controller
    {
        private readonly ICategoriaCargoService _categoriaService;
        private readonly IMapper _mapper;

        public CategoriaCargoController(ICategoriaCargoService categoriaService, IMapper mapper)
        {
            _mapper = mapper;
            _categoriaService = categoriaService;
        }

        public async Task<IActionResult> IndexCategoriaCargo()
        {
            List<CCatcarDto> lista = [];

            var response = await _categoriaService.ObtenerTodos<APIResponse>();
            if (response != null && response.IsExitoso)
            {
                lista = JsonConvert.DeserializeObject<List<CCatcarDto>>(Convert.ToString(response.Resultado));
            }

            return View(lista);
        }
    }
}
