using static WebPersonal_Utilidad.DS;

namespace WebPersonal_MVC.Models
{
    public class APIRequest
    {
        public APITipo APITipo { get; set; } = APITipo.GET;

        public string? Url { get; set; }

        public object? Datos { get; set; }
        
        public string Token {  get; set; } 
    }
}
