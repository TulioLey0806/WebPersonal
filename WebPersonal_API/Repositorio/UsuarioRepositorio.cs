using WebPersonal_API.Datos;
using WebPersonal_API.Modelos;
using WebPersonal_API.Modelos.Dto;
using WebPersonal_API.Repositorio.IRepositorio;

namespace WebPersonal_API.Repositorio
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly PersonalDbContext _db;

        public UsuarioRepositorio(PersonalDbContext db)
        {
            _db = db;
        }

        public bool IsUsuarioUnico(string UserName)
        {
            var usuario = _db.Usuarios.FirstOrDefault(u => u.UserName == UserName);
            if (usuario == null)
            {
                return true;
            }
            return false;
        }

        public Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {

        }

        public async Task<Usuario> Registrar(RegistroRequestDto registroRequestDto)
        {
            Usuario usuario = new() 
            {
                UserName = registroRequestDto.UserName, 
                Password = registroRequestDto.Password,
                Nombres = registroRequestDto.Nombres,
                Rol = registroRequestDto.Rol
            };
            await _db.Usuarios.AddAsync(usuario);
            await _db.SaveChangesAsync();
            usuario.Password = "";
            return usuario;


        }
    }
}
