using Web.Epsas.Context.Abstract;
using Web.Epsas.Context.Model;

namespace Web.Epsas.Context.Services.Interfaces
{
    public interface IManagerRest
    {

        Task<ResponseBase> GetAsyncFromQuery(string pControlador, object parametros);
        Task<Response<T>> GetAsyncFromPath<T>(string pControlador, object parametros);
        Task<Response<T>> GetAsync<T>(string pControlador);
        Task<Response<T>> PostAsync<T>(string pControlador, object parametros);
        Task<Response<T>> DeleteAsync<T>(string pControlador, int id);
        Task<Response<T>> PutAsync<T>(string pControlador, object parametros, int id);
        Task<Response<T>> PostLoginAsync<T>(string pControlador, AuthModel modelAuth);

    }
}
