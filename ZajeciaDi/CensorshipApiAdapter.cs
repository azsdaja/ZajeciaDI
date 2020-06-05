using System;

namespace ZajeciaDi
{
    public class CensorshipApiAdapter : ICensorshipApi
    {
        private CensorshipApi _censorshipApi = new CensorshipApi();

        public bool IsValid(string text)
        {
            return _censorshipApi.IsValid(text);
        }
    }

    public class DateTimeAdapter : IDateTimeProvider
    {
        public DateTime GetCurrentTime()
        {
            return DateTime.Now;
        }
    }

    public class ShipArrivalDateValidator
    {
        public bool IsCorrect(DateTime estimatedTimeOfArrival)
        {
            if (estimatedTimeOfArrival < DateTime.Now.AddDays(-90))
                return false;
            if (estimatedTimeOfArrival > DateTime.Now.AddDays(90))
                return false;

            return true;
        }
    }

    public interface IDateTimeProvider
    {
        public DateTime GetCurrentTime();
    }
}