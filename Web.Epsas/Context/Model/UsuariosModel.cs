using System.Text.Json.Serialization;

namespace Web.Epsas.Context.Model
{
    public class UsuariosModel
    {

        public int IdsegUsuarios { get; set; }
        public string Login { get; set; }
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public int IdsegRoles { get; set; }
        public int IdcEstadousaurio { get; set; }

        public string NombreRol { get; set; }

        [JsonIgnore]
        public string Password { get; set; }
        [JsonIgnore]
        public RolModel Roles { get; set; }
    }
}
