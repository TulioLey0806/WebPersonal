using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebPersonal_MVC.Models;
using WebPersonal_MVC.Models.Dto;
using WebPersonal_MVC.Services.IServices;
using WebPersonal_Utilidad;

namespace WebPersonal_MVC.Controllers
{
    public class ProvinciaController : Controller
    {
        private readonly IProvinciaService _provinciaService;
        private readonly IMapper _mapper;
        
        public ProvinciaController(IProvinciaService provinciaService, IMapper mapper)
        {
            _mapper = mapper;
            _provinciaService = provinciaService;
        }
        
        public async Task<IActionResult> IndexProvincia()
        {
            List<CProvinDto> lista = [];

            var response = await _provinciaService.ObtenerTodos<APIResponse>(HttpContext.Session.GetString(DS.SessionToken));
            if(response != null && response.IsExitoso)
            {
                lista = JsonConvert.DeserializeObject<List<CProvinDto>>(Convert.ToString(response.Resultado));
            }

            return View(lista);
        }

        // Get
        public async Task<IActionResult> CreateProvincia() 
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProvincia(CProvinCreateDto modelo) 
        {
           if(ModelState.IsValid) 
           {
                var reponse = await _provinciaService.Crear<APIResponse>(modelo, HttpContext.Session.GetString(DS.SessionToken));
                if (reponse != null && reponse.IsExitoso) 
                {
                    TempData["exitoso"] = "Provincia Creada Satifactoriamente";
                    return RedirectToAction(nameof(IndexProvincia));
                }
           }
            return View(modelo);             
        }

        public async Task<IActionResult> ActualizarProvincia(string codProvin)
        {
            var response = await _provinciaService.Obtener<APIResponse>(codProvin, HttpContext.Session.GetString(DS.SessionToken));
            if(response != null && response.IsExitoso)
            {
                CProvinDto model = JsonConvert.DeserializeObject<CProvinDto>(Convert.ToString(response.Resultado));
                return View(_mapper.Map<CProvinUpdateDto>(model));
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ActualizarProvincia(CProvinUpdateDto modelo)
        {
            if(ModelState.IsValid)
            {
                var response = await _provinciaService.Actualizar<APIResponse>(modelo, HttpContext.Session.GetString(DS.SessionToken));
                if(response != null && response.IsExitoso)
                {
                    TempData["exitoso"] = "Provincia Actualizada Satifactoriamente";
                    return RedirectToAction(nameof(IndexProvincia));
                }
            }
            return View(modelo);
        }

        public async Task<IActionResult> RemoverProvincia(string codProvin)
        {
            var response = await _provinciaService.Obtener<APIResponse>(codProvin, HttpContext.Session.GetString(DS.SessionToken));
            if (response != null && response.IsExitoso)
            {
                CProvinDto model = JsonConvert.DeserializeObject<CProvinDto>(Convert.ToString(response.Resultado));
                return View(model);
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoverProvincia(CProvinDto modelo)
        {
            var response = await _provinciaService.Remover<APIResponse>(modelo.CodProvin, HttpContext.Session.GetString(DS.SessionToken));
            if (response != null && response.IsExitoso)
            {
                TempData["exitoso"] = "Provincia Eliminada Satifactoriamente";
                return RedirectToAction(nameof(IndexProvincia));
            }
            TempData["error"] = "Ha ocurrido un error al eliminar la Provincia";
            return View(modelo);
        }
    }
}
