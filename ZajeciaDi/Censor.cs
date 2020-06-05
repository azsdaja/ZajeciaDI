using System;
using System.Collections.Generic;
using System.Linq;
using Serilog;

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

        private List<string> _acceptedTextsCache = new List<string>();

        private ICensorshipApi _censorshipApi;
        private ILogger _logger;

        public Censor(ICensorshipApi censorshipApi, ILogger logger)
        {
            Console.WriteLine("tu konstruktor Censora");

            _censorshipApi = censorshipApi;

            _logger = logger;
        }

        public bool IsAcceptable(string text)
        {
            bool? isValidExternal = null;

            if (_acceptedTextsCache.Contains(text))
            {
                isValidExternal = true;
            }

            if (!isValidExternal.HasValue)
            {
                try
                {
                    isValidExternal = _censorshipApi.IsValid(text);
                }
                catch (Exception e)
                {
                    _logger.Error("nie mogę się połączyć z serwisem censorship");
                }
            }

            if (isValidExternal.Value == true)
            {
                _acceptedTextsCache.Add(text);
            }

            return isValidExternal.Value && _badWords.Any(text.Contains);
        }
    }
}