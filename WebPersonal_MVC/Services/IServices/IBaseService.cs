using WebPersonal_MVC.Models;

namespace WebPersonal_MVC.Services.IServices
{
    public interface IBaseService
    {
        public APIResponse responseModel {  get; set; }

        Task<T> SendAsync<T>(APIRequest apiRequest);
    }
}
