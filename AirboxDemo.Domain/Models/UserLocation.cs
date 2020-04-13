using System;

namespace AirboxDemo.Domain.Models
{
    public class UserLocation
    {
        public int UserId { get; set; }
        public DateTime RecordedAt { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string Area { get; set; }
    }
}
