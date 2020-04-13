using AirboxDemo.Domain.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace AirboxDemo.Domain.DataLayer
{
    public class MongoLocationDataAccess : BaseMongoDb, ILocationDataAccess
    {
        public MongoLocationDataAccess(IMongoClient mongoClient) : base(mongoClient)
        {
        }

        public IEnumerable<UserLocation> GetCurrentLocationForAllUsers()
        {
            throw new NotImplementedException();
        }

        public UserLocation GetCurrentLocationForUser(int userId)
        {
            throw new NotImplementedException();
        }

        public List<UserLocation> GetLocationHistoryForUser(int userId)
        {
            throw new NotImplementedException();
        }

        public bool SetCurrentLocation(UserLocation userLocation)
        {
            throw new NotImplementedException();
        }

        public List<UserLocation> GetCurrentLocationForAllUsersByAreaName(string area)
        {
            throw new NotImplementedException();
        }

        public List<UserLocation> GetCurrentLocationForAllUsersByAreaCoordinates(AreaCoordinates area)
        {
            throw new NotImplementedException();
        }

        protected override string DatabaseName
        {
            get { return "CustomerLocation"; }
        }

        protected override void RegisterClassMaps()
        {
            base.RegisterClassMaps();
        }
    }
}
