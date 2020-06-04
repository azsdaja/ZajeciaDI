using System.Collections.Generic;
using System.Linq;

namespace ZajeciaDi
{
    public static class Censor
    {
        private static List<string> _badWords = new List<string>
        {
            "motyla noga",
            "kurde",
            "głupek"
        };

        public static bool IsAcceptable(string text)
        {
            bool isValidExternal = CensorshipApi.IsValid(text);

            return isValidExternal && _badWords.Any(text.Contains);
        }
    }
}