using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebPersonal_API.Datos;
using WebPersonal_API.Modelos;
using WebPersonal_API.Modelos.Dto;
using WebPersonal_API.Repositorio.IRepositorio;

namespace WebPersonal_API.Repositorio
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly PersonalDbContext _db;
        private string secretKey;
        private readonly UserManager<UsuarioAplicacion> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public UsuarioRepositorio(PersonalDbContext db, IConfiguration configuration, UserManager<UsuarioAplicacion> userManager,
                                  IMapper mapper, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            secretKey = configuration.GetValue<string>("ApiSettings:Secret");
            _userManager = userManager;
            _mapper = mapper;
            _roleManager = roleManager;
        }

        public bool IsUsuarioUnico(string UserName)
        {
            var usuario = _db.UsuariosAplicacion.FirstOrDefault(u => u.UserName.ToLower() == UserName.ToLower());
            if (usuario == null)
            {
                return true;
            }
            return false;
        }

        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            var usuario = await _db.UsuariosAplicacion.FirstOrDefaultAsync(u => u.UserName.ToLower() == loginRequestDto.UserName.ToLower());

            bool isValido = await _userManager.CheckPasswordAsync(usuario, loginRequestDto.Password);

            if (usuario == null || isValido == false)
            {
                return new LoginResponseDto()
                {
                    Token = "",
                    Usuario = null
                };
            }
            
            // Se almacena el Rol del usuario
            var roles = await _userManager.GetRolesAsync(usuario);
            // Si el usuario existe Generamos JW Token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                   new Claim(ClaimTypes.Name, usuario.UserName),
                   new Claim(ClaimTypes.Role, roles.FirstOrDefault())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            LoginResponseDto loginResponseDto = new()
            {
                Token = tokenHandler.WriteToken(token),
                Usuario = _mapper.Map<UsuarioDto>(usuario)
            };
            return loginResponseDto;
        }

        public async Task<UsuarioDto> Registrar(RegistroRequestDto registroRequestDto)
        {
            UsuarioAplicacion usuario = new()
            {
                UserName = registroRequestDto.UserName,
                NormalizedUserName = registroRequestDto.UserName.ToUpper(),
                Email = registroRequestDto.UserName,
                NormalizedEmail = registroRequestDto.UserName.ToUpper(),
                Nombres = registroRequestDto.Nombres,
                Cod_ident = registroRequestDto.Cod_ident,
                Cod_reeup = registroRequestDto.Cod_reeup
            };

            try
            {
                var resultado = await _userManager.CreateAsync(usuario, registroRequestDto.Password);
                if (resultado.Succeeded)
                {
                    // En caso que no exista el Rol Admin se crea.
                    if (!_roleManager.RoleExistsAsync("Admin").GetAwaiter().GetResult())
                    {
                        await _roleManager.CreateAsync(new IdentityRole("Admin"));
                    }
                    // En caso que no exista el Rol Admin se crea.
                    if (!_roleManager.RoleExistsAsync("Invitado").GetAwaiter().GetResult())
                    {
                        await _roleManager.CreateAsync(new IdentityRole("Invitado"));
                    }

                    await _userManager.AddToRoleAsync(usuario, "Invitado");
                    var usuarioAp = _db.UsuariosAplicacion.FirstOrDefault(u=> u.UserName == registroRequestDto.UserName);
                    return _mapper.Map<UsuarioDto>(usuarioAp);
                }
            }
            catch (Exception)
            {
                throw;
            }
            
            return new UsuarioDto();

        }
    }
}
