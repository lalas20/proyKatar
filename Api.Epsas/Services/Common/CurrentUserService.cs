using Api.Epsas.Interfaces.common;

namespace Api.Epsas.Services.Common
{
    public class CurrentUserService : ICurrentUserService
    {
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {

            if (httpContextAccessor.HttpContext.User.Claims.ToList().Count > 0)
            {
                Login = httpContextAccessor.HttpContext.User.Claims.ToList().Where(c => c.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name")).FirstOrDefault().Value;
                Nombre = httpContextAccessor.HttpContext.User.Claims.ToList().Where(c => c.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")).FirstOrDefault().Value;
                IdsegUsuarios = Convert.ToInt32(httpContextAccessor.HttpContext.User.FindFirst("IdsegUsuarios").Value);
                IdsegRoles = Convert.ToInt32(httpContextAccessor.HttpContext.User.FindFirst("IdsegRoles").Value);
                IdcEstadousuario = Convert.ToInt32(httpContextAccessor.HttpContext.User.FindFirst("IdcEstadousaurio").Value);
                Rol = httpContextAccessor.HttpContext.User.FindFirst("Rol").Value;
                Descripcion = httpContextAccessor.HttpContext.User.FindFirst("Descripcion").Value;
                Cedula = httpContextAccessor.HttpContext.User.FindFirst("Cedula").Value;
            }
        }

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
