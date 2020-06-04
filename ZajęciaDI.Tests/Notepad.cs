using System;
using Autofac;
using NUnit.Framework;
using ZajeciaDi;

namespace ZajęciaDI.Tests
{
    public class Notepad
    {
        [Test]
        public void Test()
        {
            var facebookService = new FacebookService();

            var firstPerson = new Person();
            facebookService.Register(firstPerson);

            facebookService.PostContent(firstPerson, "Cześć wam");
            facebookService.PostContent(firstPerson, "Cześć wam głupek");

            Assert.That(firstPerson.Contents.Count, Is.EqualTo(1));
        }
    }
}
