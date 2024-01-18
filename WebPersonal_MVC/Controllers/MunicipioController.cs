using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using WebPersonal_MVC.Models;
using WebPersonal_MVC.Models.Dto;
using WebPersonal_MVC.Models.ViewModel;
using WebPersonal_MVC.Services;
using WebPersonal_MVC.Services.IServices;

namespace WebPersonal_MVC.Controllers
{
    public class MunicipioController : Controller
    {
        private readonly IMunicipioService _municipioService;
        private readonly IProvinciaService _provinciaService;
        private readonly IMapper _mapper;

        public MunicipioController(IMunicipioService municipioService, IProvinciaService provinciaService, IMapper mapper)
        {
            _municipioService = municipioService;
            _provinciaService = provinciaService;
            _mapper = mapper;
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

        //Get
        public async Task<IActionResult> CreateMunicipio()
        {
            MunicipioViewModel municipioVM = new();
            var response = await _provinciaService.ObtenerTodos<APIResponse>();
            if (response != null && response.IsExitoso)
            {
                municipioVM.ProvinciaList = JsonConvert.DeserializeObject<List<CProvinDto>>(Convert.ToString(response.Resultado))
                                            .Select(v => new SelectListItem
                                            {
                                                Text = v.NomProvin,
                                                Value = v.CodProvin
                                            });
            }
            return View(municipioVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateMunicipio(MunicipioViewModel modelo)
        {
            if (ModelState.IsValid)
            {
                var response = await _municipioService.Crear<APIResponse>(modelo.CMunici);
                if (response !=null && response.IsExitoso)
                {
                    return RedirectToAction(nameof(IndexMunicipio));
                }
                else
                {
                    if (response.ErrorMessages.Count > 0)
                    {
                        ModelState.AddModelError("ErrorMessages", response.ErrorMessages.FirstOrDefault());
                    }
                }
            }

            var res = await _provinciaService.ObtenerTodos<APIResponse>();
            if (res != null && res.IsExitoso)
            {
                modelo.ProvinciaList = JsonConvert.DeserializeObject<List<CProvinDto>>(Convert.ToString(res.Resultado))
                                            .Select(v => new SelectListItem
                                            {
                                                Text = v.NomProvin,
                                                Value = v.CodProvin
                                            });
            }

            return View(modelo);
        }


    }
}
