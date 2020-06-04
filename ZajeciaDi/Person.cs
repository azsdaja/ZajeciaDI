using System.Collections.Generic;

namespace ZajeciaDi
{
    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Person> Friends { get; set; } = new List<Person>();
        public List<Content> Contents { get; set; } = new List<Content>();
    }
}