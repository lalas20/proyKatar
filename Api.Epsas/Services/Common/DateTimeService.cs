using Api.Epsas.Interfaces.common;

namespace Api.Epsas.Services.Common
{
    public class DateTimeService : IDateTimeService
    {
        public DateTime NowUtc => DateTime.UtcNow;
    }
}
