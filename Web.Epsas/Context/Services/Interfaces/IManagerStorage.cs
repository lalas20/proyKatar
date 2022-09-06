using Web.Epsas.Dtos;

namespace Web.Epsas.Context.Services.Interfaces
{
    public interface IManagerStorage
    {
        Task<bool> SetValue<T>(string pName, T pValue);
        Task<bool> SetValue(string pName, string pValue);
        Task<string> GetValue(string pNane);
        Task<T> GetValue<T>(string pName);
        Task<bool> DeleteValue(string pName);

        Task<LoginResponse> DatosUsuario();

    }
}
