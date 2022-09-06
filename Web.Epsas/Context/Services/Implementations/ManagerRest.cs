using Blazored.LocalStorage;
using System.Net.Http.Headers;
using System.Text.Json;
using Web.Epsas.Context.Abstract;
using Web.Epsas.Context.Model;
using Web.Epsas.Context.Services.Interfaces;

namespace Web.Epsas.Context.Services.Implementations
{
    public class ManagerRest : IManagerRest
    {

        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILocalStorageService _localStorageService;

        public ManagerRest(IHttpClientFactory httpClientFactory, ILocalStorageService localStorageService)
        {
            _httpClientFactory = httpClientFactory;
            _localStorageService = localStorageService;
        }

        public async Task<Response<T>> DeleteAsync<T>(string pControlador, int id)
        {
            var cliente = await GetCliente();
            HttpResponseMessage result = new();
            result = await cliente.DeleteAsync(pControlador + "/" + id);

            if (result.IsSuccessStatusCode)
            {
                using var responseStream = await result.Content.ReadAsStreamAsync();
                var data = await JsonSerializer.DeserializeAsync<Response<T>>(responseStream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                data.State = State.Success;
                data.Message = $"Se Elimino el registro {id} correctamente";
                return data;
            }
            else
            {
                var errorres = await result.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<Response<T>>(errorres);
            }
        }

        public async Task<Response<T>> GetAsync<T>(string pControlador)
        {

            HttpResponseMessage result = new();
            var value = string.Empty;
            var cliente = await GetCliente();

            result = await cliente.GetAsync(pControlador);

            if (result.IsSuccessStatusCode)
            {
                using var responseStream = await result.Content.ReadAsStreamAsync();
                var data = await JsonSerializer.DeserializeAsync<Response<T>>(responseStream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                data.State = State.Success;
                return data;
            }
            else
            {
                var errorres = await result.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<Response<T>>(errorres);
            }
        }

        public async Task<Response<T>> GetAsyncFromPath<T>(string pControlador, object parametros)
        {
            HttpResponseMessage result = new();
            var value = string.Empty;
            var cliente = await GetCliente();

            if (parametros.GetType().IsClass)
            {
                var jObj = JsonSerializer.Serialize<object>(parametros);
                foreach (var item in JsonSerializer.Deserialize<JsonElement>(jObj).EnumerateObject())
                {
                    value += "/" + item.Value.ToString();
                };
            }
            else
            {
                value = "/" + parametros;
            }

            result = await cliente.GetAsync(pControlador + value);

            if (result.IsSuccessStatusCode)
            {
                using var responseStream = await result.Content.ReadAsStreamAsync();
                var data = await JsonSerializer.DeserializeAsync<Response<T>>(responseStream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                data.State = State.Success;
                return data;
            }
            else
            {
                var errorres = await result.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<Response<T>>(errorres);
            }
        }

        public async Task<ResponseBase> GetAsyncFromQuery(string pControlador, object parametros)
        {

            HttpResponseMessage result = new();
            var value = string.Empty;
            var cliente = await GetCliente();

            var jObj = JsonSerializer.Serialize<object>(parametros);
            foreach (var item in JsonSerializer.Deserialize<JsonElement>(jObj).EnumerateObject())
            {
                value += item.Name + "=" + item.Value.ToString() + "&";
            };
            value = value.Substring(0, value.Length - 1);
            result = await cliente.GetAsync(pControlador + "?" + value);

            if (result.IsSuccessStatusCode)
            {
                using var responseStream = await result.Content.ReadAsStreamAsync();
                var data = await JsonSerializer.DeserializeAsync<ResponseBase>(responseStream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                data.State = State.Success;
                return data;
            }
            else
            {
                var errorres = await result.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<ResponseBase>(errorres);
            }

        }

        public async Task<Response<T>> PostLoginAsync<T>(string pControlador, AuthModel modelAuth)
        {
            HttpResponseMessage result = new();
            var value = string.Empty;
            var cliente = _httpClientFactory.CreateClient("Api");
            var vresp = await cliente.PostAsJsonAsync(pControlador, modelAuth);

            if (vresp.IsSuccessStatusCode)
            {
                using var responseStream = await vresp.Content.ReadAsStreamAsync();                
                var data = await JsonSerializer.DeserializeAsync<Response<T>> (responseStream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });                
                return data;
            }
            else
            {
                var errorres = await result.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<Response<T>>(errorres);
            }

        }

        public async Task<Response<T>> PostAsync<T>(string pControlador, object parametros)
        {
            var cliente = await GetCliente();
            HttpResponseMessage result = new();
            result = await cliente.PostAsJsonAsync(pControlador, parametros);

            if (result.IsSuccessStatusCode)
            {
                using var responseStream = await result.Content.ReadAsStreamAsync();
                var data = await JsonSerializer.DeserializeAsync<Response<T>>(responseStream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                data.State = State.Success;
                data.Message = "Se registro el registro correctamente.";
                return data;
            }
            else
            {
                var errorres = await result.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<Response<T>>(errorres);
            }
        }

        public async Task<Response<T>> PutAsync<T>(string pControlador, object parametros, int id)
        {
            var cliente = await GetCliente();
            HttpResponseMessage result = new();
            result = await cliente.PutAsJsonAsync(pControlador + "/" + id, parametros);

            if (result.IsSuccessStatusCode)
            {
                using var responseStream = await result.Content.ReadAsStreamAsync();
                var data = await JsonSerializer.DeserializeAsync<Response<T>>(responseStream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                data.State = State.Success;
                data.Message = $"Se actualizó el registro Nro : {id} correctamente.";
                return data;
            }
            else
            {
                var errorres = await result.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<Response<T>>(errorres);
            }
        }

        private async Task<HttpClient> GetCliente()
        {
            var cliente = _httpClientFactory.CreateClient("Api");
            cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _localStorageService.GetItemAsStringAsync("KEYACCES"));
            return cliente;
        }

    }
}
