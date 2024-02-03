using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebPersonal_MVC.Models;
using WebPersonal_MVC.Models.Dto;
using WebPersonal_MVC.Models.ViewModel;
using WebPersonal_MVC.Services;
using WebPersonal_MVC.Services.IServices;
using WebPersonal_Utilidad;

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

        [Authorize(Roles = "Invitado,Admin")]
        public async Task<IActionResult> IndexCategoriaCargo(int pageNumber = 1)
        {
            List<CCatcarDto> lista = [];
            CategoriaCargoPaginadoViewModel categoriaCargoVM = new();

            // Garantizo que sea 1 en caso que sea negativo
            if (pageNumber < 1) pageNumber = 1;

            var response = await _categoriaService.ObtenerTodosPaginado<APIResponse>(HttpContext.Session.GetString(DS.SessionToken), pageNumber, 5);
            if (response != null && response.IsExitoso)
            {
                lista = JsonConvert.DeserializeObject<List<CCatcarDto>>(Convert.ToString(response.Resultado));
                categoriaCargoVM = new CategoriaCargoPaginadoViewModel()
                {
                    CategoriaCargoList = lista,
                    PageNumber = pageNumber,
                    TotalPaginas = JsonConvert.DeserializeObject<int>(Convert.ToString(response.TotalPaginas))
                };

                if (pageNumber > 1) categoriaCargoVM.Previo = "";
                if (categoriaCargoVM.TotalPaginas <= pageNumber) categoriaCargoVM.Siguiente = "disabled";
            }
            return View(categoriaCargoVM);
        }

    }
}
