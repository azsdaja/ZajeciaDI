using System.Threading;

namespace ZajeciaDi
{
    public class CensorshipApi
    {
        public bool IsValid(string text)
        {
            Thread.Sleep(1000);
            return text.Contains("Cześć");
            // tutaj odnosimy się do zewnętrznej usługi
        }
    }
}