using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Text.Json;
using Web.Epsas.Context.Model;
using Web.Epsas.Context.Services.Interfaces;
using Web.Epsas.Dtos;

namespace Web.Epsas.Context.Services.Implementations
{
    public class ManagerAuthorize : AuthenticationStateProvider, IManagerAuthorize
    {

        private readonly ILocalStorageService _localStorageService;
        private readonly string KEYACCES = "KEYACCES";
        private AuthenticationState anonimo => new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

        public ManagerAuthorize(ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
        }

        public async Task LoginAsync(Response<LoginResponse> response)
        {
            await _localStorageService.SetItemAsStringAsync(KEYACCES, response.Message);
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(ParseClaimsFromJwt(response.Message))))));
        }

        public async Task LogoutnAsync()
        {

            await _localStorageService.RemoveItemAsync(KEYACCES);
            NotifyAuthenticationStateChanged(Task.FromResult(anonimo));

        }

        private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var claims = new List<Claim>();
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

            keyValuePairs.TryGetValue(ClaimTypes.Role, out object roles);

            if (roles != null)
            {
                if (roles.ToString().Trim().StartsWith("["))
                {
                    var parsedRoles = JsonSerializer.Deserialize<string[]>(roles.ToString());

                    foreach (var parsedRole in parsedRoles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, parsedRole));
                    }
                }
                else
                {
                    claims.Add(new Claim(ClaimTypes.Role, roles.ToString()));
                }

                keyValuePairs.Remove(ClaimTypes.Role);
            }

            claims.AddRange(keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString())));

            return claims;
        }

        private byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }

        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var vToken = await _localStorageService.GetItemAsStringAsync(KEYACCES);
            if (string.IsNullOrEmpty(vToken))
            {
                return anonimo;
            }
            var identity = new ClaimsIdentity(ParseClaimsFromJwt(vToken), "Acceso");
            return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(identity)));
        }


    }
}
