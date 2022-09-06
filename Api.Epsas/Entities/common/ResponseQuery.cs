namespace Api.Epsas.Entities.common
{
    public class ResponseQuery<T> : ResponseBase
    {
        public List<T>? Data { get; set; }
    }
}
