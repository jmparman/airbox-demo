using AirboxDemo.Domain.Models;
using EnsureThat;
using System;
using System.Collections.Generic;

namespace AirboxDemo.Domain.DataLayer
{
    public class DummyLocationDataAccess : ILocationDataAccess
    {
        public IEnumerable<UserLocation> GetCurrentLocationForAllUsers()
        {
            return CurrentLocationsAllUsers;
        }

        public UserLocation GetCurrentLocationForUser(int userId)
        {
            EnsureArg.IsGt(userId, 0, nameof(userId));

            // get most recent location from store
            return new UserLocation
            {
                UserId = userId,
                Longitude = 12.34,
                Latitude = -23.45,
                Area = "Here",
                RecordedAt = DateTime.Now.AddMinutes(-5)
            };
        }

        public List<UserLocation> GetLocationHistoryForUser(int userId)
        {
            EnsureArg.IsGt(userId, 0, nameof(userId));

            return UserLocationHistory;
        }

        public bool SetCurrentLocation(UserLocation userLocation)
        {
            EnsureArg.IsGt(userLocation.UserId, 0, nameof(userLocation.UserId));
            EnsureArg.IsGt(userLocation.Longitude, -180, nameof(userLocation.Longitude));
            EnsureArg.IsLt(userLocation.Longitude, 180, nameof(userLocation.Longitude));
            EnsureArg.IsGt(userLocation.Latitude, -90, nameof(userLocation.Latitude));
            EnsureArg.IsLt(userLocation.Latitude, 90, nameof(userLocation.Latitude));
            EnsureArg.IsNotNullOrWhiteSpace(userLocation.Area, nameof(userLocation.Area));

            userLocation.RecordedAt = DateTime.Now;

            // add to store

            return true;
        }

        public List<UserLocation> GetCurrentLocationForAllUsersByAreaName(string area)
        {
            EnsureArg.IsNotNullOrWhiteSpace(area, nameof(area));

            return CurrentLocationOfUsersInArea;
        }

        public List<UserLocation> GetCurrentLocationForAllUsersByAreaCoordinates(AreaCoordinates area)
        {
            EnsureArg.IsNotNull(area, nameof(area));
            EnsureArg.IsGt(area.LongitudeMin, -180, nameof(area.LongitudeMin));
            EnsureArg.IsLt(area.LongitudeMin, 180, nameof(area.LongitudeMin));
            EnsureArg.IsGt(area.LongitudeMax, -180, nameof(area.LongitudeMax));
            EnsureArg.IsLt(area.LongitudeMax, 180, nameof(area.LongitudeMax));
            EnsureArg.IsGt(area.LatitudeMin, -180, nameof(area.LatitudeMin));
            EnsureArg.IsLt(area.LatitudeMin, 180, nameof(area.LatitudeMin));
            EnsureArg.IsGt(area.LatitudeMax, -180, nameof(area.LatitudeMax));
            EnsureArg.IsLt(area.LatitudeMax, 180, nameof(area.LatitudeMax));

            return CurrentLocationOfUsersInArea;
        }

        private List<UserLocation> CurrentLocationsAllUsers = new List<UserLocation>
        {
            new UserLocation
            {
                UserId = 2,
                Longitude = 10,
                Latitude = 10,
                Area = "Here",
                RecordedAt = DateTime.Now.AddHours(-2)
            },
            new UserLocation
            {
                UserId = 4,
                Longitude = -30.55,
                Latitude = -30.55,
                Area = "There",
                RecordedAt = DateTime.Now.AddHours(-2)
            },
            new UserLocation
            {
                UserId = 6,
                Longitude = 0,
                Latitude = 0,
                Area = "Foo",
                RecordedAt = DateTime.Now.AddHours(-2)
            },
            new UserLocation
            {
                UserId = 7,
                Longitude = -25,
                Latitude = -25,
                Area = "Somewhere",
                RecordedAt = DateTime.Now.AddHours(-2)
            },
            new UserLocation
            {
                UserId = 8,
                Longitude = 42,
                Latitude = -42,
                Area = "Anywhere",
                RecordedAt = DateTime.Now.AddHours(-2)
            },
        };

        private List<UserLocation> UserLocationHistory = new List<UserLocation>
        {
            new UserLocation
            {
                UserId = 2,
                Longitude = 10,
                Latitude = 10,
                Area = "Here",
                RecordedAt = DateTime.Now.AddHours(-2)
            },
            new UserLocation
            {
                UserId = 2,
                Longitude = 12,
                Latitude = 12,
                Area = "Here",
                RecordedAt = DateTime.Now.AddDays(-1)
            },
            new UserLocation
            {
                UserId = 2,
                Longitude = 50,
                Latitude = 50,
                Area = "There",
                RecordedAt = DateTime.Now.AddDays(-5)
            }
        };

        private List<UserLocation> CurrentLocationOfUsersInArea = new List<UserLocation>
        {
            new UserLocation
            {
                UserId = 2,
                Longitude = 10.12,
                Latitude = 10.12,
                Area = "Here",
                RecordedAt = DateTime.Now.AddHours(-2)
            },
            new UserLocation
            {
                UserId = 3,
                Longitude = 10.11,
                Latitude = 10.11,
                Area = "Here",
                RecordedAt = DateTime.Now.AddHours(-1)
            },
            new UserLocation
            {
                UserId = 5,
                Longitude = 10.1,
                Latitude = 10.1,
                Area = "Here",
                RecordedAt = DateTime.Now.AddHours(-3)
            }
        };
    }
}
