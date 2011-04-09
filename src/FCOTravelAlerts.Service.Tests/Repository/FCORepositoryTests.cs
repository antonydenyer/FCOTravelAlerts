using System;
using System.Linq;
using FCOTravelAlerts.Service.Entity;
using FCOTravelAlerts.Service.Repository;
using NUnit.Framework;
using Rhino.Mocks;

namespace FCOTravelAlerts.Service.Tests.Repository
{
    [TestFixture]
    public class FCORepositoryTests
    {
        private FCORepository _fcoRepository;

        [SetUp]
        private void Setup()
        {
            var webRequest = MockRepository.GenerateStub<IXmlWebRequest>();
            webRequest.Stub(x => x.Get(Arg<string>.Is.Anything))
                .Return(FeedData.Data);

            _fcoRepository = new FCORepository(webRequest);
        }

        [Test]
        public void Can_map_response_from_requst()
        {
            var items = _fcoRepository.Get();

            Assert.That(items.Count(),Is.EqualTo(20));
            Assert.That(items.First().DatePublished.Date, Is.EqualTo(new DateTime(2011,04,09).Date));
        }

        [Test]
        public void Can_hit_yahoo_pipes()
        {
            var items = new FCORepository(new XmlWebRequest()).Get();
            items.ToList().ForEach(Console.WriteLine);
        }

        [Test]
        public void can_parse_hash_tag()
        {
            var hashtag = new HashTagParser().Parse(new Item("#libya blah", DateTime.Now));
            Assert.That(hashtag, Is.EqualTo("libya"));
        }
    }
}
