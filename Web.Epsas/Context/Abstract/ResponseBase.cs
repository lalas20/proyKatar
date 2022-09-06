namespace Web.Epsas.Context.Abstract
{

    public enum State
    {
        Success = 1,
        Warning = 2,
        Error = 3,
        NoData = 4
    }

    public abstract class ResponseBase
    {
        public State State { get; set; }
        public string? Message { get; set; }        
               
    }
}
