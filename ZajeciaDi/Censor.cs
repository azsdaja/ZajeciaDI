using System.Collections.Generic;
using System.Linq;
using Autofac;

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

        public Censor(IContainer container)
        {
            // _censorshipApi = censorshipApi;
            _censorshipApi = container.Resolve<ICensorshipApi>();
        }

        public bool IsAcceptable(string text)
        {
            bool isValidExternal = _censorshipApi.IsValid(text);

            return isValidExternal && _badWords.Any(text.Contains);
        }
    }
}