using System.ComponentModel.DataAnnotations;

namespace Web.Epsas.Context.Model
{
    public class UsuariosDto
    {


        public int IdsegUsuarios { get; set; }
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Recursos.Validation))]
        public string Login { get; set; }
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Recursos.Validation))]
        public string Cedula { get; set; }
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Recursos.Validation))]
        public string Nombre { get; set; }
        public int IdsegRoles { get; set; }
        public int IdcEstadousaurio { get; set; }
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Recursos.Validation))]
        public string NombreRol { get; set; }
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Recursos.Validation))]
        public string Password { get; set; }
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Recursos.Validation))]
        public string Estado { get; set; }
    }
}
