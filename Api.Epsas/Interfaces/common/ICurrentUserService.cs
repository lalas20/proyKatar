namespace Api.Epsas.Interfaces.common
{
    public interface ICurrentUserService
    {

     
        public int IdsegUsuarios { get; set; }
        public int IdsegRoles { get; set; }
        public int IdcEstadousuario { get; set; }
        public string Nombre { get; set; }
        public string Rol { get; set; }
        public string Descripcion { get; set; }
        public string Login { get; set; }
        public string Cedula { get; set; }

    }
}
