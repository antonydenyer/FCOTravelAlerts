using System;
using System.Collections.Generic;
using System.Xml;
using FCOTravelAlerts.Service.Entity;
using FCOTravelAlerts.Service.Repository;

namespace FCOTravelAlerts.Service
{
    public interface IFCORepository
    {
        IEnumerable<Item> Get();
    }

    public class FCORepository : IFCORepository
    {
        private readonly IXmlWebRequest _webRequest;

        public const string FEED = "http://pipes.yahoo.com/pipes/pipe.run?_id=ac45e9eb9b0174a4e53f23c4c9903c3f&_render=rss&statustitle=logo&username=fcotravel";
       
        public FCORepository(IXmlWebRequest webRequest)
        {
            _webRequest = webRequest;
        }

        public IEnumerable<Item> Get()
        {
            var data = _webRequest.Get(FEED);
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
}