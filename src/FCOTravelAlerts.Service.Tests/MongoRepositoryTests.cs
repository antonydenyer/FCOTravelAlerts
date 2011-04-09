using MongoDB.Driver;
using NUnit.Framework;

namespace FCOTravelAlerts.Service.Tests
{
    [TestFixture]
    public class MongoRepositoryTests
    {
        [Test]
        public void Can_connect_to_mongo()
        {
            new MongoSpike().Connect();
        }
    }

    public class MongoSpike
    {
        public void Connect()
        {
            using (Mongo mongo = new Mongo("Server=10.98.0.241:27017"))
            {
                mongo.Connect();
            }
        }
    }
}