using WebPersonal_API.Modelos.Dto;

namespace WebPersonal_API.Datos
{
    public static class C_catcarStore
    {
        public static List<C_catcarDto> c_catcarList =
        [
              new() {Cod_catcar="00", Nom_catcar="Sin Clasificador"},
              new() {Cod_catcar="01", Nom_catcar="Funcionario"}
        ];

    }
}
