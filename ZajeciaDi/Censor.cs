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

        public Censor(ICensorshipApi censorshipApi = null)
        {
            _censorshipApi = censorshipApi;
            if (_censorshipApi == null)
            {
                _censorshipApi = new CensorshipApi();
            }
        }

        public bool IsAcceptable(string text)
        {
            bool isValidExternal = _censorshipApi.IsValid(text);

            return isValidExternal && _badWords.Any(text.Contains);
        }
    }
}