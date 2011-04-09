using System.Collections.Generic;
using FCOTravelAlerts.Service.Entity;

namespace FCOTravelAlerts.Service.Repository
{
    public interface IUserRepository
    {
        IEnumerable<User> GetUsersForCountry(string tag);
    }
}