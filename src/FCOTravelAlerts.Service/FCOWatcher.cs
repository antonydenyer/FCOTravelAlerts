using System;
using System.Linq;
using FCOTravelAlerts.Service.Notifier;

namespace FCOTravelAlerts.Service
{
    public class FCOWatcher
    {
        private readonly IFCORepository _fcoRepository;
        private readonly IFCONotifier _fcoNotifer;
        private DateTime _lastNotified;

        public FCOWatcher(IFCORepository fcoRepository,IFCONotifier fcoNotifer)
        {
            _fcoRepository = fcoRepository;
            _fcoNotifer = fcoNotifer;
            _lastNotified = DateTime.Now.AddDays(-1);
        }

        public void WatchTill(DateTime finishTime)
        {
            while (DateTime.Now < finishTime)
            {
                var newItems = _fcoRepository.Get()
                    .Where(x => x.DatePublished > _lastNotified)
                    .OrderBy(x => x.DatePublished)
                    .ToList();

                if (newItems.Count() == 0) continue;

                _lastNotified = newItems.First().DatePublished;

                foreach (var item in newItems)
                {
                    _fcoNotifer.NotifySubsribersAbout(item);
                }
            }
        }
    }
}