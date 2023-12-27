using WebPersonal_API.Modelos.Dto;

namespace WebPersonal_API.Datos
{
    public static class C_catcarStore
    {
        public static List<CCatcarDto> ccatcarList =
        [
              new() { CodCatcar = "00", NomCatcar = "Sin Clasificador"},
              new() { CodCatcar = "01", NomCatcar = "Funcionario"}
        ];

    }
}
