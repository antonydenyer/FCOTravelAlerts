using System.Linq;
using FCOTravelAlerts.Service.Entity;

namespace FCOTravelAlerts.Service
{
    public class HashTagParser
    {
        public string Parse(Item item)
        {
            var hashTag = item.Title.Split(new[] {' '}).Where(token => token.StartsWith("#")).FirstOrDefault();
            if (!string.IsNullOrEmpty(hashTag)) return hashTag.Trim(new[] {'#'});
            return "";
        }
    }
}