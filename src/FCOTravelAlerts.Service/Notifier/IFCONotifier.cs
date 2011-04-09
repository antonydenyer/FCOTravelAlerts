using FCOTravelAlerts.Service.Entity;

namespace FCOTravelAlerts.Service.Notifier
{
    public interface IFCONotifier
    {
        void NotifySubsribersAbout(Item item);
    }
}