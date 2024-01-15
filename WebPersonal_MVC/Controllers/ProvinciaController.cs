﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebPersonal_MVC.Models;
using WebPersonal_MVC.Models.Dto;
using WebPersonal_MVC.Services.IServices;

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

            var response = await _provinciaService.ObtenerTodos<APIResponse>();
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
                var reponse = await _provinciaService.Crear<APIResponse>(modelo);
                if (reponse != null && reponse.IsExitoso) 
                {
                    return RedirectToAction(nameof(IndexProvincia));
                }
           }
            return View(modelo);             
        }


    }
}
