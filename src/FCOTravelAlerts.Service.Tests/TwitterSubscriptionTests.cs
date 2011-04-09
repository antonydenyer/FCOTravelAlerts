using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace FCOTravelAlerts.Service.Tests
{
    [TestFixture]
    public class TwitterSubscriptionTests
    {

        [Test]
        public void should_be_able_to_connect_to_the_FCO_twitter_feed()
        {

            Assert.That(feed,Is.StringContaining());
        }
    }
}
