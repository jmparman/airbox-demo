using AirboxDemo.Service.Models;
using EnsureThat;
using System;

namespace AirboxDemo.Service
{
    public class GeolocationApiService : IGeolocationApiService
    {
        public (double, double) GetLatLongForLocation(LocationAddress location)
        {
            EnsureArg.IsNotNull(location, nameof(location));
            EnsureArg.IsNotNullOrWhiteSpace(location.Street, nameof(location.Street));
            // etc

            // do something to get the real cooridnates

            var longitude = GetRandomNumber(-180, 180);
            var latitude = GetRandomNumber(-90, 90);

            return (longitude, latitude);
        }

        private double GetRandomNumber(double min, double max)
        {
            var random = new Random();

            return random.NextDouble() * (max - min) + min;
        }
    }
}
