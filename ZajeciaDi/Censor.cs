using System.Collections.Generic;
using System.Linq;

namespace ZajeciaDi
{
    public class Censor : ICensor
    {
        private static List<string> _badWords = new List<string>
        {
            "motyla noga",
            "kurde",
            "głupek"
        };

        private ICensorshipApi _censorshipApi;

        public Censor(ICensorshipApi censorshipApi)
        {
            _censorshipApi = censorshipApi;
        }

        public bool IsAcceptable(string text)
        {
            bool isValidExternal = _censorshipApi.IsValid(text);

            return isValidExternal && _badWords.Any(text.Contains);
        }

        /*
         * 1. zmodyfikować kod tak, żeby można było pisać testy dodawania treści przez użytkowników BEZ UŻYWANIA RZECZYWISTEGO
         * CensorshipApi.
         *
         * wskazówka — użyć interfejsu.
         *
         * dla chętnych — nie modyfikować CensorshipApi.
         */
    }
}