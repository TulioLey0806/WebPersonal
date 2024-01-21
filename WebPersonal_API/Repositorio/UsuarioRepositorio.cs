﻿using Microsoft.EntityFrameworkCore;
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

        public UsuarioRepositorio(PersonalDbContext db, IConfiguration configuration)
        {
            _db = db;
            secretKey = configuration.GetValue<string>("ApiSettings:Secret");
        }

        public bool IsUsuarioUnico(string UserName)
        {
            var usuario = _db.Usuarios.FirstOrDefault(u => u.UserName.ToLower() == UserName.ToLower());
            if (usuario == null)
            {
                return true;
            }
            return false;
        }

        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            var usuario = await _db.Usuarios.FirstOrDefaultAsync(u => u.UserName.ToLower() == loginRequestDto.UserName.ToLower() &&
                                                                 u.Password == loginRequestDto.Password);
            if (usuario == null)
            {
                return new LoginResponseDto()
                {
                    Token = "",
                    Usuario = null
                };
            }
            // Si el usuario existe Generamos JW Token 
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                   new Claim(ClaimTypes.Name, usuario.Id.ToString()),
                   new Claim(ClaimTypes.Role, usuario.Rol)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            LoginResponseDto loginResponseDto = new()
            {
                Token = tokenHandler.WriteToken(token),
                Usuario = usuario
            };
            return loginResponseDto;
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
