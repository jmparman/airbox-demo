using AirboxDemo.Service.Models;

namespace AirboxDemo.Service
{
    public interface IGeolocationApiService
    {
        (double, double) GetLatLongForLocation(LocationAddress location);
    }
}
