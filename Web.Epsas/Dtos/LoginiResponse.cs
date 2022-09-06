namespace Web.Epsas.Dtos
{
    public class LoginResponse
    {

        public int IdsegUsuarios { get; set; }
        public string Login { get; set; }
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public int IdsegRoles { get; set; }
        public int IdcEstadousaurio { get; set; }

        public RolesResponse Roles { get; set; }

    }


    public class RolesResponse
    {
        public int IdsegRoles { get; set; }
        public string? Rol { get; set; }
        public string? Descripcion { get; set; }


    }
}
