using System.Collections.Generic;
using Autofac;

namespace ZajeciaDi
{
    public class FacebookService
    {
        private IContainer _container;
        private List<Person> AllPeople { get; set; } = new List<Person>();

        // private ICensor _censor;

        public FacebookService(IContainer container)
        {
            // _censor = ;
            _container = container;
        }

        public void Register(Person person)
        {
            AllPeople.Add(person);
        }

        public void PostContent(Person person, string text)
        {
            var censor = _container.Resolve<ICensor>();
            if (censor.IsAcceptable(text))
            {
                person.Contents.Add(new Content
                {
                    Text = text
                });
            }
        }
    }
}