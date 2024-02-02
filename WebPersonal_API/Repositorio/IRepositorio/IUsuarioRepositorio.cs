using WebPersonal_API.Modelos;
using WebPersonal_API.Modelos.Dto;

namespace WebPersonal_API.Repositorio.IRepositorio
{
    public interface IUsuarioRepositorio
    {
       bool IsUsuarioUnico(string UserName);

       Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);

       Task<UsuarioDto> Registrar(RegistroRequestDto registroRequestDto);
    }
}
