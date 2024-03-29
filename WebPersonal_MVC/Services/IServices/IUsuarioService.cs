﻿using WebPersonal_MVC.Models.Dto;

namespace WebPersonal_MVC.Services.IServices
{
    public interface IUsuarioService
    {
        Task<T> Login<T>(LoginRequestDto dto);

        Task<T> Registrar<T>(RegistroRequestDto dto);
    }
}
