using System.Security.Cryptography;
using System.Text;

namespace Api.Epsas.Helpers
{
    public static class CGeneric
    {
        public static string Md5Hash(string input)
        {
            using var md5Hash = MD5.Create();
            var sourceBytes = Encoding.UTF8.GetBytes(input);
            var hashBytes = md5Hash.ComputeHash(sourceBytes);
            var hash = BitConverter.ToString(hashBytes).Replace("-", string.Empty);

            return hash;
        }
    }

}
