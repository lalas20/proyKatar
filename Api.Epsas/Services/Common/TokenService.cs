using Api.Epsas.Interfaces.common;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Api.Epsas.Services.Common
{
    public class TokenService : ITokenService
    {


        public string BuildToken(string key, string issuer, string duration, SegUsuarios user)
        {
            TimeSpan ExpiryDuration = new TimeSpan(0, Int32.Parse(duration), 0);
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Nombre),
                new Claim(ClaimTypes.NameIdentifier, user.Login),
                new Claim("cedula", user.Cedula),
                new Claim("IdsegUsuarios", user.IdsegUsuarios.ToString()),
                new Claim("IdsegRoles", user.IdsegRoles.ToString()),
                new Claim("Rol", user.Roles.Rol),
                new Claim("Descripcion", user.Roles.Descripcion),
                new Claim("IdcEstadousaurio", user.IdcEstadousaurio.ToString())
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new JwtSecurityToken(issuer, issuer, claims,
                expires: DateTime.Now.Add(ExpiryDuration), signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }
    }
}
