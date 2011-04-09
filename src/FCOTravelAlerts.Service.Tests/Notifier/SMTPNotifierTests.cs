using System;
using System.Collections.Generic;
using FakeItEasy;
using FCOTravelAlerts.Service.Entity;
using FCOTravelAlerts.Service.Notifier;
using FCOTravelAlerts.Service.Repository;
using NUnit.Framework;

namespace FCOTravelAlerts.Service.Tests.Notifier
{
    [TestFixture]
    public class SMTPNotifierTests
    {
        [Test]
        public void test_email()
        {
            var userRepository = A.Fake<IUserRepository>();
            IEnumerable<User> newItems = new [] { new User {Email = "antony.denyer@7digital.com"}};
            A.CallTo(() => userRepository.GetUsersForCountry(A<string>.Ignored))
                .Returns(newItems);
            
            var notifier = new SMTPNotifier(userRepository,new HashTagParser());

            notifier.NotifySubsribersAbout(new Item("#Libya rules",DateTime.Now));
        }
    }
}
