using Web.Epsas.Context.Model;
using Web.Epsas.Dtos;

namespace Web.Epsas.Context.Services.Interfaces
{
    public interface IManagerAuthorize
    {
        Task LoginAsync(Response<LoginResponse> response);

        Task LogoutnAsync();

    }
}
