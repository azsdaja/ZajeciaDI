using System.Collections.Generic;

namespace ZajeciaDi
{
    public class FacebookService
    {
        private List<Person> AllPeople { get; set; } = new List<Person>();

        public void Register(Person person)
        {
            AllPeople.Add(person);
        }

        public void PostContent(Person person, string text)
        {
            if (Censor.IsAcceptable(text))
            {
                person.Contents.Add(new Content
                {
                    Text = text
                });
            }
        }
    }
}