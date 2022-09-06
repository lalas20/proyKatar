namespace Api.Epsas.Interfaces.common
{
    public interface ITokenService
    {
        string BuildToken(string key, string issuer, string duration, SegUsuarios user);

    }
}
