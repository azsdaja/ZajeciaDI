using System.Collections.Generic;

namespace ZajeciaDi
{
    public class FacebookService
    {
        private List<Person> AllPeople { get; set; } = new List<Person>();

        private ICensor _censor;

        public FacebookService(ICensor censor = null)
        {
            _censor = censor;
            if (_censor == null)
            {
                _censor = new Censor();
            }
        }

        public void Register(Person person)
        {
            AllPeople.Add(person);
        }

        public void PostContent(Person person, string text)
        {
            if (_censor.IsAcceptable(text))
            {
                person.Contents.Add(new Content
                {
                    Text = text
                });
            }
        }
    }
}