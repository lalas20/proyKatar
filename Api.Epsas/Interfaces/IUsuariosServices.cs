using Api.Epsas.Entities.common;
using Api.Epsas.Entities.Dtos;

namespace Api.Epsas.Interfaces
{
    public interface IUsuariosServices
    {
        Task<ResponseEntity> Login(AuthModel login);

        Task<int> Create(UsuariosDto req, CancellationToken cancellation = default);

        Task<int> Delete(int id, CancellationToken cancellation = default);

        List<SegUsuarios> GetAll();

        SegUsuarios GetById(int id);

        Task<int> Update(UsuariosDto req, CancellationToken cancellation = default);

        public List<SegRoles> GetAllRoles();

        public Task<List<ParamClasificador>> GetEstados();
    }
}
