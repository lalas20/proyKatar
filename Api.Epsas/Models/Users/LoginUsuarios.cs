using System.ComponentModel.DataAnnotations;

namespace Api.Epsas.Models.Users
{
    public class LoginUsuarios
    {

        [Required]
        public string Usuario { get; set; }

        [Required]
        public string Password { get; set; }

    }
}
