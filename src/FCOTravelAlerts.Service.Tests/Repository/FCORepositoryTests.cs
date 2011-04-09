using System;
using System.Linq;
using FCOTravelAlerts.Service.Repository;
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
}
