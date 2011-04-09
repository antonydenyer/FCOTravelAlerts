using System.Xml;

namespace FCOTravelAlerts.Service.Repository
{
    public interface IXmlWebRequest
    {
        XmlDocument Get(string uri);
    }
}