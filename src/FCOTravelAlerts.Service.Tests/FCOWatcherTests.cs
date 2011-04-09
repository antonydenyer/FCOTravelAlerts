using System;
using System.Collections.Generic;
using FakeItEasy;
using FCOTravelAlerts.Service.Entity;
using FCOTravelAlerts.Service.Notifier;
using NUnit.Framework;

namespace FCOTravelAlerts.Service.Tests
{
    public class FCOWatcherTests
    {
        private IFCORepository _fcoRepository;
        private IFCONotifier _fcoNotifier;

        [SetUp]
        public void Setup()
        {
            _fcoRepository = A.Fake<IFCORepository>();
            _fcoNotifier = A.Fake<IFCONotifier>();
        }

        [Test]
        public void Should_get_from_the_repo()
        {
            new FCOWatcher(_fcoRepository, _fcoNotifier).WatchTill(DateTime.Now.AddSeconds(1));

            A.CallTo(() => _fcoRepository.Get()).MustHaveHappened(Repeated.AtLeast.Twice);
        }

        [Test]
        public void Should_notify_if_new_items()
        {
            var newItems = new [] { new Item("", DateTime.Now.AddDays(1))};
            A.CallTo(() => _fcoRepository.Get()).Returns(newItems).Once();

            new FCOWatcher(_fcoRepository, _fcoNotifier).WatchTill(DateTime.Now.AddSeconds(1));

            A.CallTo(() => _fcoNotifier.NotifySubsribersAbout(newItems[0])).MustHaveHappened(Repeated.Exactly.Once);
        }
        
    }
}