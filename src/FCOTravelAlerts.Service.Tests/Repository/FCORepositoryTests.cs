using System.Collections.Generic;
using System.Linq;
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
            XDocument data = _webRequest.Get(FEED);
            var items = new List<Item>();
            foreach (var item in data.Descendants("item"))
            {
                items.Add(new Item());
            }

            
            return items;
        }
    }

    public class Item
    {
    }

    public interface IXmlWebRequest
    {
         XDocument Get(string uri);
    }
}
