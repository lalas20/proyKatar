using System.Text.Json.Serialization;

namespace Api.Epsas.Entities.Dtos
{
    public class UsuariosDto
    {

        public int IdsegUsuarios { get; set; }
        public string Login { get; set; }
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public int IdsegRoles { get; set; }
        public int IdcEstadousaurio { get; set; }
               
        public string Password { get; set; }

    }
}
