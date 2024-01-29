using Asp.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebPersonal_API.Modelos;
using WebPersonal_API.Modelos.Dto;
using WebPersonal_API.Repositorio.IRepositorio;

namespace WebPersonal_API.Controllers.v1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepositorio _usuarioRepo;
        protected APIResponse _response;

        public UsuarioController(IUsuarioRepositorio usuarioRepo)
        {
            _usuarioRepo = usuarioRepo;
            _response = new APIResponse();
        }

        [HttpPost("login")]  //  /api/usuario/login
        public async Task<IActionResult> Login([FromBody] LoginRequestDto modelo)
        {
            var loginResponse = await _usuarioRepo.Login(modelo);
            if (loginResponse == null || string.IsNullOrEmpty(loginResponse.Token))
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsExitoso = false;
                _response.ErrorMessages.Add("Usuario o Contraseña son Incorrectos");
                return BadRequest(_response);
            }
            _response.StatusCode = HttpStatusCode.OK;
            _response.IsExitoso = true;
            _response.Resultado = loginResponse;
            return Ok(_response);
        }

        [HttpPost("registrar")]  //  /api/usuario/registrar
        public async Task<IActionResult> Registrar([FromBody] RegistroRequestDto modelo)
        {
            bool isUsuarioUnico = _usuarioRepo.IsUsuarioUnico(modelo.UserName);
            if (!isUsuarioUnico)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsExitoso = false;
                _response.ErrorMessages.Add("Usuario ya Existe");
                return BadRequest(_response);
            }
            var usuario = await _usuarioRepo.Registrar(modelo);
            if (usuario == null)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsExitoso = false;
                _response.ErrorMessages.Add("Error al Registrar el Usuario");
                return BadRequest(_response);
            }
            _response.StatusCode = HttpStatusCode.OK;
            _response.IsExitoso = true;
            return Ok(_response);
        }

    }
}
