using Api.Epsas.Entities.common;
using Api.Epsas.Interfaces;
using Api.Epsas.Interfaces.common;
using Microsoft.AspNetCore.Authorization;

namespace Api.Epsas.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [AllowAnonymous]
    public class LoginController : ControllerBase
    {

        private readonly IUsuariosServices _userService;
        private readonly ITokenService _token;
        private readonly IConfiguration _config;

        public LoginController(IUsuariosServices userService, ITokenService token, IConfiguration config)
        {
            _userService = userService;
            _token = token;
            _config = config;
        }

        [HttpPost]
        public async Task<ResponseEntity> Login(AuthModel request)
        {
            var respuesta = await _userService.Login(request);
            if (respuesta.State != State.Success)
            {
                respuesta.Message = "Usuario o Contraseña Incorrectos";
                return respuesta;
            }

            if ((respuesta.Data as SegUsuarios).IdcEstadousaurio != 21)
            {
                respuesta.State = State.Warning;
                respuesta.Data = null;
                respuesta.Message = "El usuario se encuentra Inactivo";
            }

            var tokenkon = _token.BuildToken(_config["Jwt:key"], _config["Jwt:Issuer"], _config["Jwt:Duration"], respuesta.Data as SegUsuarios);

            respuesta.Data = respuesta.Data;
            respuesta.State = State.Success;
            respuesta.Message = tokenkon;

            return respuesta;
        }

    }
}
