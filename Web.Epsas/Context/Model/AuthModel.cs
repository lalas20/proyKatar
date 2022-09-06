using System.ComponentModel.DataAnnotations;

namespace Web.Epsas.Context.Model
{
    public class AuthModel
    {
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Recursos.Validation))]
        public string UserName { get; set; }
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Recursos.Validation))]
        public string Password { get; set; }

    }
}
