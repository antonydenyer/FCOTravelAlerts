using System;
using System.IO;
using System.Net;
using System.Xml;

namespace FCOTravelAlerts.Service.Repository
{
    public class XmlWebRequest: IXmlWebRequest
    {
        public XmlDocument Get(string uri)
        {
            var httpWebRequest = HttpWebRequest.Create(uri);
            httpWebRequest.Method = "GET";
            using(var response = httpWebRequest.GetResponse())
            {
                var xmlDocument = new XmlDocument();
                var xmlString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                xmlDocument.LoadXml(xmlString);
                //Console.WriteLine(xmlString);
                return xmlDocument;
            }
        }
    }
}