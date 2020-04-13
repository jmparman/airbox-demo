using AirboxDemo.Domain.Models;
using System.Collections.Generic;

namespace AirboxDemo.Domain.DataLayer
{
    public interface ILocationDataAccess
    {
        bool SetCurrentLocation(UserLocation userLocation);

        UserLocation GetCurrentLocationForUser(int userId);

        List<UserLocation> GetLocationHistoryForUser(int userId);

        IEnumerable<UserLocation> GetCurrentLocationForAllUsers();

        List<UserLocation> GetCurrentLocationForAllUsersByAreaName(string area);

        List<UserLocation> GetCurrentLocationForAllUsersByAreaCoordinates(AreaCoordinates area);
    }
}
