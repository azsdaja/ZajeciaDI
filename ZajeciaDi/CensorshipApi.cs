using System.Threading;

namespace ZajeciaDi
{
    public class CensorshipApi
    {
        public static bool IsValid(string text)
        {
            Thread.Sleep(1000);
            return text.Contains("Cześć");
            // tutaj odnosimy się do zewnętrznej usługi
        }
    }
}