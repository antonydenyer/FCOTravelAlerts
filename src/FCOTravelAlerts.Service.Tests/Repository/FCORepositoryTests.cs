using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using NUnit.Framework;
using Rhino.Mocks;

namespace FCOTravelAlerts.Service.Tests.Repository
{
    [TestFixture]
    public class FCORepositoryTests
    {

        [Test]
        public void Can_map_response_from_requst()
        {
            var webRequest = MockRepository.GenerateStub<IXmlWebRequest>();
            webRequest.Stub(x => x.Get(Arg<string>.Is.Anything))
                .Return(FeedData.Data);

            var fcoRepository = new FCORepository(webRequest);
            var items = fcoRepository.Get();

            Assert.That(items.Count(),Is.EqualTo(20));
            Assert.That(items.First().DatePublished.Date, Is.EqualTo(new DateTime(2011,04,09).Date));
        }
    }

    public class FCORepository
    {
        private readonly IXmlWebRequest _webRequest;

        private const string FEED = "http://pipes.yahoo.com/pipes/pipe.run?_id=ac45e9eb9b0174a4e53f23c4c9903c3f&_render=rss&statustitle=logo&username=fcotravel";
       
        public FCORepository(IXmlWebRequest webRequest)
        {
            _webRequest = webRequest;
        }

        public IEnumerable<Item> Get()
        {
            XmlDocument data = _webRequest.Get(FEED);
            var items = new List<Item>();
            foreach (XmlNode item in data.SelectNodes("//item"))
            {
                var title = item.SelectSingleNode("title").InnerText;
                var datePublished = DateTime.Parse(item.SelectSingleNode("pubDate").InnerText);
                items.Add(new Item(title,datePublished));
            }

            
            return items;
        }
    }

    public class Item
    {
        private readonly string _title;
        private readonly DateTime _datePublished;

        public Item(string title, DateTime datePublished)
        {
            _title = title;
            _datePublished = datePublished;
        }

        public DateTime DatePublished
        {
            get { return _datePublished; }
        }

        public string Title
        {
            get { return _title; }
        }
    }

    public interface IXmlWebRequest
    {
        XmlDocument Get(string uri);
    }
}
