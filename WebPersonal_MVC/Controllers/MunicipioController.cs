using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebPersonal_MVC.Models;
using WebPersonal_MVC.Models.Dto;
using WebPersonal_MVC.Services;
using WebPersonal_MVC.Services.IServices;

namespace WebPersonal_MVC.Controllers
{
    public class MunicipioController : Controller
    {
        private readonly IMunicipioService _municipioService;
        private readonly IMapper _mapper;

        public MunicipioController(IMunicipioService municipioService, IMapper mapper)
        {
            _mapper = mapper;
            _municipioService = municipioService;
        }

        public async Task<IActionResult> IndexMunicipio()
        {
            List<CMuniciDto> lista = [];

            var response = await _municipioService.ObtenerTodos<APIResponse>();
            if (response != null && response.IsExitoso)
            {
                lista = JsonConvert.DeserializeObject<List<CMuniciDto>>(Convert.ToString(response.Resultado));
            }

            return View(lista);
        }
    }
}
