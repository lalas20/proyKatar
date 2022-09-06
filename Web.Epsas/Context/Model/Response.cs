using Web.Epsas.Context.Abstract;

namespace Web.Epsas.Context.Model
{
    public class Response<T> : ResponseBase
    {       
        public T Data { get; set; }
    }
}
