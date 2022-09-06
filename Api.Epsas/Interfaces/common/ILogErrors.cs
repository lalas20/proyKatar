using Npgsql;

namespace Api.Epsas.Interfaces.common
{
    public interface ILogErrors
    {
        
        void Register(Exception e);
    }
}
