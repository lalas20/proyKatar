using Blazored.LocalStorage;
using Web.Epsas.Context.Services.Interfaces;
using Web.Epsas.Dtos;

namespace Web.Epsas.Context.Services.Implementations
{
    public class ManagerStorage : IManagerStorage
    {

        private readonly ILocalStorageService _localStorageService;
        public ManagerStorage(ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
        }

        public async Task<bool> DeleteValue(string pName)
        {
            bool result = false;
            try
            {
                await _localStorageService.RemoveItemAsync(pName);
                result = true;
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }

        public async Task<string> GetValue(string pNane)
        {
            return await _localStorageService.GetItemAsStringAsync(pNane);
        }

        public async Task<T> GetValue<T>(string pName)
        {
            return await _localStorageService.GetItemAsync<T>(pName);
        }

        public async Task<bool> SetValue<T>(string pName, T pValue)
        {
            bool result = false;
            try
            {
                await _localStorageService.SetItemAsync(pName, pValue);
                result = true;
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }

        public async Task<bool> SetValue(string pName, string pValue)
        {
            bool result = false;
            try
            {
                await _localStorageService.SetItemAsStringAsync(pName, pValue);
                result = true;
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }

        public async Task<LoginResponse> DatosUsuario()
        {
            return await _localStorageService.GetItemAsync<LoginResponse>("USER");
        }

    }
}
